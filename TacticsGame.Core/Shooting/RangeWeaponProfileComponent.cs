using TacticsGame.Core.Weapons.Profiles;

namespace TacticsGame.Core.Shooting;

public struct RangeWeaponProfileComponent
{
    public int Range { get; set; }
    public int NumberOfShots { get; set; }
    public int BallisticSkill { get; set; }
    public int Strength { get; set; }
    public int AP { get; set; }
    public int Damage { get; set; }

    public RangeWeaponProfileComponent
        (int range, int numberOfShots, int ballisticSkill, int strength, int ap, int damage)
    {
        Range = range;
        NumberOfShots = numberOfShots;
        BallisticSkill = ballisticSkill;
        Strength = strength;
        AP = ap;
        Damage = damage;
    }

    public RangeWeaponProfileComponent(WeaponProfile weaponProfile)
    {
        Range = weaponProfile.Range;
        NumberOfShots = weaponProfile.NumberOfShots;
        BallisticSkill = weaponProfile.BallisticSkill;
        Strength = weaponProfile.Strength;
        AP = weaponProfile.AP;
        Damage = weaponProfile.Damage;
    }
}