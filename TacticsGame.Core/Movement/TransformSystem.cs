﻿using Leopotam.EcsLite;
using TacticsGame.Core.Movement.Pathfinding;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Movement;

public class TransformSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _currentUnitFilter;

    private EcsPool<MovementComponent> _movements;
    private EcsPool<LocationComponent> _locations;
    private EcsPool<PathComponent> _path;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _movements = world.GetPool<MovementComponent>();
        _locations = world.GetPool<LocationComponent>();
        _path = world.GetPool<PathComponent>();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().Inc<UnitProfileComponent>().End();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            Moving(currentUnit);
        }
    }

    private void Moving(int currentUnit)
    {
        ref var movementComponent = ref _movements.Get(currentUnit);

        if (!movementComponent.IsMoving) return;

        var remainingMovement = movementComponent.RemainingMovement;

        ref var currentUnitLocationComponent = ref _locations.Get(currentUnit);

        if (_path.Has(currentUnit))
        {
            ref var pathComponent = ref _path.Get(currentUnit);
            var path = pathComponent.Path;

            if (path.Count - 1 <= remainingMovement && path.Count > 1)
            {
                movementComponent.RemainingMovement -= path.Count - 1;

                currentUnitLocationComponent.Location = path[^1].Location;
                pathComponent.Path.Clear();
            }
        }

        if (movementComponent.RemainingMovement == 0) movementComponent.IsMoving = false;
    }
}