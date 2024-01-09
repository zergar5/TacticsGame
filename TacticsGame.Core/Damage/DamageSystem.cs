using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Context;
using TacticsGame.Core.Mechanics;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Damage;

public class DamageSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly DiceRoller _diceRoller;

    private EcsFilter _currentRangeWeapon;

    private EcsPool<RangeWeaponProfileComponent> _rangeWeaponProfiles;
    private EcsPool<RangeWeaponComponent> _rangeWeapons;
    private EcsPool<TargetComponent> _targets;
    private EcsPool<UnitProfileComponent> _unitProfiles;
    private EcsPool<ShootingResultComponent> _hitsResults;
    private EcsPool<WoundsComponent> _wounds;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _currentRangeWeapon = world.Filter<CurrentUnitMarker>().Inc<RangeWeaponProfileComponent>().End();

        _rangeWeaponProfiles = world.GetPool<RangeWeaponProfileComponent>();
        _rangeWeapons = world.GetPool<RangeWeaponComponent>();
        _targets = world.GetPool<TargetComponent>();
        _unitProfiles = world.GetPool<UnitProfileComponent>();
        _hitsResults = world.GetPool<ShootingResultComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentWeapon in _currentRangeWeapon)
        {
            ref var rangeWeaponComponent = ref _rangeWeapons.Get(currentWeapon);

            if (rangeWeaponComponent is not { MadeShot: false, IsShooting: true }) continue;

            var rangeWeaponProfileComponent = _rangeWeaponProfiles.Get(currentWeapon);
            var unitProfileComponent = _unitProfiles.Get(_targets.Get(currentWeapon).UnitId);

            var ap = rangeWeaponProfileComponent.AP;
            var save = unitProfileComponent.Save;

            MakeSaveRolls(currentWeapon, ap, save);
            RemoveСasualties(currentWeapon);

            rangeWeaponComponent.IsShooting = false;
            rangeWeaponComponent.MadeShot = true;
        }
    }

    private void MakeSaveRolls(int currentWeapon, int ap, int save)
    {
        ref var hitsResultComponent = ref _hitsResults.Get(currentWeapon);

        var rollsResult = _diceRoller.RollD6(hitsResultComponent.SuccessfulWounds);

        var numberOfScoredSaves = rollsResult.Count(rollResult => save >= rollResult - ap);

        hitsResultComponent.Сasualties = hitsResultComponent.SuccessfulWounds - numberOfScoredSaves;
    }

    private void RemoveСasualties(int currentWeapon)
    {
        var hitsResultComponent = _hitsResults.Get(currentWeapon);
        ref var woundsComponent = ref _wounds.Get(currentWeapon);

        if (woundsComponent.RemainingWounds > hitsResultComponent.Сasualties)
        {
            woundsComponent.RemainingWounds -= hitsResultComponent.Сasualties;
        }
        else
        {
            woundsComponent.RemainingWounds = 0;
        }
    }
}