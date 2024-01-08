using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Handlers.StateHandlers;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Scene;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Shooting;

public class RangeTargetingSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly MouseTargetPositionHandler _mouseTargetPositionHandler;
    [EcsInject] private readonly ShootingStateHandler _shootingStateHandler;
    [EcsInject] private readonly Cartographer _cartographer;

    private EcsFilter _currentUnit;
    private EcsFilter _currentRangeWeapon;
    private EcsFilter _battlefieldFilter;

    private EcsPool<UnitProfileComponent> _units;
    private EcsPool<RangeWeaponProfileComponent> _rangeWeaponsProfiles;
    private EcsPool<RangeWeaponComponent> _rangeWeapons;
    private EcsPool<EligibleTargetsComponent> _eligibleTargets;
    private EcsPool<TargetComponent> _targets;
    private EcsPool<BattlefieldComponent> _battlefields;

    private BattlefieldTiles _battlefieldTiles;

    public void Init(IEcsSystems systems)
    {
        foreach (var battlefield in _battlefieldFilter)
        {
            _battlefieldTiles = _battlefields.Get(battlefield).Map;
        }
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentRangeWeapon in _currentRangeWeapon)
        {
            var eligibleTargetsTiles = _eligibleTargets.Get(currentRangeWeapon).EligibleTargetsTiles;

            if (_shootingStateHandler.GetState())
            {
                var targetTileIndex = _cartographer.FindTileIndex(_mouseTargetPositionHandler.GetPosition());

                if (targetTileIndex.row != -1 && targetTileIndex.column != -1)
                {
                    var targetTile = _battlefieldTiles[targetTileIndex];

                    if (eligibleTargetsTiles.Contains(targetTile))
                    {
                        _rangeWeapons.Get(currentRangeWeapon).IsShooting = true;

                        //Доделать поиск id юнита
                        _targets.Get(currentRangeWeapon).UnitId = 1;
                    }
                }
            }
        }
    }
}