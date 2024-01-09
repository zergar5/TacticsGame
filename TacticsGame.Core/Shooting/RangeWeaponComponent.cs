namespace TacticsGame.Core.Shooting;

public struct RangeWeaponComponent
{
    public bool IsShooting { get; set; }
    public bool MadeShot { get; set; }

    public RangeWeaponComponent()
    {
        IsShooting = false;
        MadeShot = false;
    }
}