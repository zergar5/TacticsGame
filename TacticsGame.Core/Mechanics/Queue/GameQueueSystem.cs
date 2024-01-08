using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Context;
using TacticsGame.Core.Handlers.UnitStateHandlers;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Movement.Pathfinding;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Mechanics.Queue;

public class GameQueueSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly EntityBuilder _entityBuilder;
    [EcsInject] private readonly GameQueue _queue;
    [EcsInject] private readonly MovingStateHandler _movingStateHandler;

    private EcsFilter _currentUnitFilter;
    private EcsFilter _unitsFilter;
    private EcsPool<CurrentUnitMarker> _currentUnitMarker;
    private EcsPool<UnitProfileComponent> _units;
    private EcsPool<MovementComponent> _movements;
    private EcsPool<ReachableTilesComponent> _reachableTiles;
    private EcsPool<PathComponent> _pathComponents;

    private Dictionary<int, int> _unitsMovements = new();

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().Inc<UnitProfileComponent>().End();
        _unitsFilter = world.Filter<UnitProfileComponent>().End();

        _currentUnitMarker = world.GetPool<CurrentUnitMarker>();
        _units = world.GetPool<UnitProfileComponent>();
        _movements = world.GetPool<MovementComponent>();
        _reachableTiles = world.GetPool<ReachableTilesComponent>();
        _pathComponents = world.GetPool<PathComponent>();

        MakeUnitsMovementsDictionary();
        _queue.SetUnits(_unitsMovements);

        var currentUnit = _queue.NextUnit();

        _entityBuilder.Set(currentUnit, new CurrentUnitMarker());
        _movements.Get(currentUnit).IsMoving = true;
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            ref var movementComponent = ref _movements.Get(currentUnit);
            movementComponent.IsMoving = _movingStateHandler.GetState();

            if (movementComponent.RemainingMovement == 0)
            {
                var nextUnit = _queue.NextUnit();

                movementComponent.IsMoving = false;

                _currentUnitMarker.Copy(currentUnit, nextUnit);
                _currentUnitMarker.Del(currentUnit);
                _reachableTiles.Del(currentUnit);
                _pathComponents.Del(currentUnit);

                movementComponent = ref _movements.Get(nextUnit);

                movementComponent.RemainingMovement = movementComponent.Movement;
                
            }
        }
    }

    public void MakeUnitsMovementsDictionary()
    {
        _unitsMovements.Clear();

        foreach (var unit in _unitsFilter)
        {
            _unitsMovements.Add(unit, _movements.Get(unit).Movement);
        }
    }
}