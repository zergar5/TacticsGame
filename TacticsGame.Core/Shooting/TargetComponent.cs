namespace TacticsGame.Core.Shooting;

public struct TargetComponent
{
    public int UnitId { get; set; }

    public TargetComponent(int unitId)
    {
        UnitId = unitId;
    }
}