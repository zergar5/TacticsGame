using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Context;
using TacticsGame.Core.Damage;
using TacticsGame.Core.Handlers.StateHandlers;
using TacticsGame.Core.Handlers.WeaponChangedHandlers;
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
    [EcsInject] private readonly WeaponsChangedHandler _weaponsChooseHandler;

    private EcsWorld _world;

    private EcsFilter _currentUnitFilter;
    private EcsFilter _unitsFilter;
    private EcsFilter _currentWeaponFilter;
    private EcsFilter _weaponsFilter;

    private EcsPool<CurrentUnitMarker> _currentUnitMarker;
    private EcsPool<CurrentWeaponMarker> _currentWeaponMarker;

    private EcsPool<UnitTurnStateComponent> _unitsTurnStates;
    private EcsPool<MovementComponent> _movements;
    private EcsPool<ReachableTilesComponent> _reachableTiles;
    private EcsPool<PathComponent> _pathComponents;
    private EcsPool<WoundsComponent> _wounds;

    private EcsPool<OwnershipComponent> _ownerships;

    private EcsPool<RangeWeaponComponent> _rangeWeapons;

    private readonly Dictionary<int, int> _unitsMovements = new();

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _currentUnitFilter = _world.Filter<CurrentUnitMarker>().Inc<UnitProfileComponent>().End();
        _unitsFilter = _world.Filter<UnitProfileComponent>().End();

        _currentWeaponFilter = _world.Filter<CurrentWeaponMarker>().Inc<RangeWeaponProfileComponent>().End();
        _weaponsFilter = _world.Filter<RangeWeaponProfileComponent>().End();

        _currentUnitMarker = _world.GetPool<CurrentUnitMarker>();
        _currentWeaponMarker = _world.GetPool<CurrentWeaponMarker>();

        _units = _world.GetPool<UnitProfileComponent>();
        _unitsTurnStates = _world.GetPool<UnitTurnStateComponent>();
        _movements = _world.GetPool<MovementComponent>();
        _reachableTiles = _world.GetPool<ReachableTilesComponent>();
        _pathComponents = _world.GetPool<PathComponent>();
        _wounds = _world.GetPool<WoundsComponent>();

        _ownerships = _world.GetPool<OwnershipComponent>();

        _rangeWeapons = _world.GetPool<RangeWeaponComponent>();
        
        MakeUnitsMovementsDictionary();
        _queue.SetUnits(_unitsMovements);

        var currentUnit = _queue.NextUnit();

        _entityBuilder.Set(currentUnit, new CurrentUnitMarker());
    }

    public void Run(IEcsSystems systems)
    {
        MakeUnitsMovementsDictionary();
        _queue.UpdateOrder(_unitsMovements);

        foreach (var currentUnit in _currentUnitFilter)
        {
            var chosenWeapon = _weaponsChooseHandler.GetId();

            if (chosenWeapon != -1)
            {
                if (!_rangeWeapons.Get(chosenWeapon).MadeShot)
                {
                    _entityBuilder.Set(chosenWeapon, new CurrentWeaponMarker());
                }
            }

            foreach (var currentWeapon in _currentWeaponFilter)
            {
                if(_rangeWeapons.Get(currentWeapon).MadeShot) _currentWeaponMarker.Del(currentWeapon);
            }

            RemoveDeadUnits();

            var isMadeTurn = _madeTurnStateHandler.GetState();
            
            if (!isMadeTurn) continue;

            _unitsTurnStates.Get(currentUnit).MadeTurn = isMadeTurn;

            PassTurn(currentUnit);

            if (CheckForNextRound()) ResetTurnStates();
            
            var nextUnit = _queue.NextUnit();

            _currentUnitMarker.Copy(currentUnit, nextUnit);
            _currentUnitMarker.Del(currentUnit);

            ResetMovement(nextUnit);
            ResetShooting(nextUnit);
        }
    }

    private void PassTurn(int unit)
    {
        _reachableTiles.Del(unit);
        _pathComponents.Del(unit);
    }

    private void RemoveDeadUnits()
    {
        foreach (var unit in _unitsFilter)
        {
            if (_wounds.Get(unit).RemainingWounds != 0) continue;

            _world.DelEntity(unit);
            _queue.RemoveUnit(unit);
        }
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
        foreach (var weapon in _weaponsFilter)
        {
            if(_ownerships.Get(weapon).OwnerId == unit) _rangeWeapons.Get(weapon).MadeShot = false;
        }
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