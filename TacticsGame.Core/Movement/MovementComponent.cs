namespace TacticsGame.Core.Movement;

public struct MovementComponent
{
    public int Movement { get; set; }
    public int RemainingMovement { get; set; }

    public MovementComponent(int movement)
    {
        Movement = movement;
        RemainingMovement = movement;
    }

}