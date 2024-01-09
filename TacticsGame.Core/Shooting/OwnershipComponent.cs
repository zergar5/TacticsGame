namespace TacticsGame.Core.Shooting;

public struct OwnershipComponent
{
    public int OwnerId { get; set; }

    public OwnershipComponent()
    {
        OwnerId = -1;
    }
}