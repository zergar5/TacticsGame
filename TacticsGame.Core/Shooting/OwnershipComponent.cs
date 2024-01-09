namespace TacticsGame.Core.Shooting;

public struct OwnershipComponent
{
    public int WeaponId { get; set; }

    public OwnershipComponent()
    {
        WeaponId = -1;
    }
}