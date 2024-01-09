using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Context;
using TacticsGame.Core.Handlers.StateHandlers;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Movement.Pathfinding;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Mechanics.Queue;

public class GameQueueSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly EntityBuilder _entityBuilder;
    [EcsInject] private readonly GameQueue _queue;
    [EcsInject] private readonly MadeTurnStateHandler _madeTurnStateHandler;

    private EcsFilter _currentUnitFilter;
    private EcsFilter _unitsFilter;

    private EcsPool<CurrentUnitMarker> _currentUnitMarker;
    private EcsPool<UnitProfileComponent> _units;
    private EcsPool<UnitTurnStateComponent> _unitsTurnStates;
    private EcsPool<MovementComponent> _movements;
    private EcsPool<RangeWeaponComponent> _rangeWeapons;
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
        _unitsTurnStates = world.GetPool<UnitTurnStateComponent>();
        _movements = world.GetPool<MovementComponent>();
        _rangeWeapons = world.GetPool<RangeWeaponComponent>();
        _reachableTiles = world.GetPool<ReachableTilesComponent>();
        _pathComponents = world.GetPool<PathComponent>();

        MakeUnitsMovementsDictionary();
        _queue.SetUnits(_unitsMovements);

        var currentUnit = _queue.NextUnit();

        _entityBuilder.Set(currentUnit, new CurrentUnitMarker());
        //_movements.Get(currentUnit).IsMoving = true;
    }

    public void Run(IEcsSystems systems)
    {
        MakeUnitsMovementsDictionary();
        _queue.UpdateOrder(_unitsMovements);

        foreach (var currentUnit in _currentUnitFilter)
        {
            var isMadeTurn = _madeTurnStateHandler.GetState();
            
            if (!isMadeTurn) continue;

            _unitsTurnStates.Get(currentUnit).MadeTurn = isMadeTurn;

            PassTurn(currentUnit);

            if (CheckForNextRound()) ResetTurnStates();
            
            var nextUnit = _queue.NextUnit();

            _currentUnitMarker.Copy(currentUnit, nextUnit);
            _currentUnitMarker.Del(currentUnit);

            ResetMovement(nextUnit);
            //ResetShooting(nextUnit);
        }
    }

    private void PassTurn(int unit)
    {
        _reachableTiles.Del(unit);
        _pathComponents.Del(unit);
    }

    private bool CheckForNextRound()
    {
        foreach (var unit in _unitsFilter)
        {
            if (!_unitsTurnStates.Get(unit).MadeTurn) return false;
        }

        return true;
    }

    private void ResetTurnStates()
    {
        foreach (var unit in _unitsFilter)
        {
            _unitsTurnStates.Get(unit).MadeTurn = false;
        }
    }

    private void ResetMovement(int unit)
    {
        ref var movementComponent = ref _movements.Get(unit);

        movementComponent.RemainingMovement = movementComponent.Movement;
        movementComponent.IsMoving = false;
    }

    private void ResetShooting(int unit)
    {
        _rangeWeapons.Get(unit).MadeShot = false;
    }

    private void MakeUnitsMovementsDictionary()
    {
        _unitsMovements.Clear();

        foreach (var unit in _unitsFilter)
        {
            _unitsMovements.Add(unit, _movements.Get(unit).Movement);
        }
    }
}