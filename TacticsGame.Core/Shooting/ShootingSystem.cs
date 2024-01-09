using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Context;
using TacticsGame.Core.Mechanics;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Shooting;

public class ShootingSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly DiceRoller _diceRoller;
    [EcsInject] private readonly EntityBuilder _entityBuilder;

    private EcsFilter _currentRangeWeapon;

    private EcsPool<RangeWeaponProfileComponent> _rangeWeaponProfiles;
    private EcsPool<RangeWeaponComponent> _rangeWeapons;
    private EcsPool<TargetComponent> _targets;
    private EcsPool<UnitProfileComponent> _unitProfiles;
    private EcsPool<ShootingResultComponent> _hitsResults;

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

            if (!rangeWeaponComponent.IsShooting) continue;

            var rangeWeaponProfileComponent = _rangeWeaponProfiles.Get(currentWeapon);

            var numberOfShots = rangeWeaponProfileComponent.NumberOfShots;
            var ballisticSkill = rangeWeaponProfileComponent.BallisticSkill;

            MakeToHitRolls(currentWeapon, numberOfShots, ballisticSkill);

            ref var hitsResultComponent = ref _hitsResults.Get(currentWeapon);

            if (hitsResultComponent.SuccessfulHits != 0)
            {
                var weaponStrength = rangeWeaponProfileComponent.Strength;
                var unitToughness = _unitProfiles.Get(_targets.Get(currentWeapon).UnitId).Toughness;

                MakeToWoundRolls(currentWeapon, weaponStrength, unitToughness);

                if (hitsResultComponent.SuccessfulWounds != 0) continue;

                rangeWeaponComponent.IsShooting = false;
                rangeWeaponComponent.MadeShot = true;
            }
            else
            {
                rangeWeaponComponent.IsShooting = false;
                rangeWeaponComponent.MadeShot = true;
            }
        }
    }

    private void MakeToHitRolls(int currentWeapon, int numberOfShots, int ballisticSkill)
    {
        var rollsResult = _diceRoller.RollD6(numberOfShots);

        var numberOfScoredHits = rollsResult.Count(rollResult => rollResult >= ballisticSkill);

        _entityBuilder.Set(currentWeapon, new ShootingResultComponent(numberOfScoredHits));
    }

    private void MakeToWoundRolls(int currentWeapon, int weaponStrength, int unitToughness)
    {
        ref var hitsResultComponent = ref _hitsResults.Get(currentWeapon);

        var numberOfScoredHits = hitsResultComponent.SuccessfulHits;

        var rollsResult = _diceRoller.RollD6(numberOfScoredHits);

        var numberOfScoredWounds = 0;

        foreach (var rollResult in rollsResult)
        {
            if (weaponStrength >= unitToughness * 2)
            {
                if (rollResult >= 2) numberOfScoredWounds++;
            }
            else if (weaponStrength > unitToughness)
            {
                if (rollResult >= 3) numberOfScoredWounds++;
            }
            else if (weaponStrength == unitToughness)
            {
                if (rollResult >= 4) numberOfScoredWounds++;
            }
            else if (weaponStrength <= unitToughness * 2)
            {
                if (rollResult >= 6) numberOfScoredWounds++;
            }
            else if (weaponStrength < unitToughness)
            {
                if (rollResult >= 5) numberOfScoredWounds++;
            }
        }

        hitsResultComponent.SuccessfulWounds = numberOfScoredWounds;
    }
}