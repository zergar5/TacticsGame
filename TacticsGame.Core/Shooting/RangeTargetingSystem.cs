using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Context;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Handlers.StateHandlers;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Scene;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Shooting;

public class RangeTargetingSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly MouseTargetPositionHandler _mouseTargetPositionHandler;
    [EcsInject] private readonly Cartographer _cartographer;
    [EcsInject] private readonly EntityBuilder _entityBuilder;

    private EcsFilter _currentRangeWeapon;
    private EcsFilter _battlefieldFilter;

    private EcsPool<RangeWeaponComponent> _rangeWeapons;
    private EcsPool<EligibleTargetsComponent> _eligibleTargets;
    private EcsPool<TargetComponent> _targets;
    private EcsPool<BattlefieldComponent> _battlefields;

    private BattlefieldTiles _battlefieldTiles;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _currentRangeWeapon = world.Filter<CurrentWeaponMarker>().Inc<RangeWeaponProfileComponent>().End();
        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        _rangeWeapons = world.GetPool<RangeWeaponComponent>();
        _eligibleTargets = world.GetPool<EligibleTargetsComponent>();
        _targets = world.GetPool<TargetComponent>();
        _battlefields = world.GetPool<BattlefieldComponent>();

        foreach (var battlefield in _battlefieldFilter)
        {
            _battlefieldTiles = _battlefields.Get(battlefield).Map;
        }
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentRangeWeapon in _currentRangeWeapon)
        {
            if (!_eligibleTargets.Has(currentRangeWeapon)) continue;

            var eligibleTargetsTiles = _eligibleTargets.Get(currentRangeWeapon).EligibleTargetsTiles;

            var targetTileIndex =
                _cartographer.FindTileIndex(_mouseTargetPositionHandler.GetPosition());

            if (targetTileIndex.row == -1 || targetTileIndex.column == -1) continue;

            var targetTile = _battlefieldTiles[targetTileIndex];

            if (!eligibleTargetsTiles.Contains(targetTile)) continue;

            _rangeWeapons.Get(currentRangeWeapon).IsShooting = true;

            if (_targets.Has(currentRangeWeapon))
            {
                _targets.Get(currentRangeWeapon).UnitId = _cartographer.FindUnitId(targetTile);
            }
            else
            {
                _entityBuilder.Set(currentRangeWeapon,
                    new TargetComponent(_cartographer.FindUnitId(targetTile)));
            }
        }
    }
}