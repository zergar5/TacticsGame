namespace TacticsGame.Core.Movement;

public struct MovementComponent
{
    public int Range { get; set; }
    public int RemainingRange { get; set; }

    public MovementComponent(int range)
    {
        Range = range;
    }

}