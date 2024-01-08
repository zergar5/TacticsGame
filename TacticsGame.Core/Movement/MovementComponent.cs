namespace TacticsGame.Core.Movement;

public struct MovementComponent
{
    public bool IsMoving { get; set; }
    public int Movement { get; set; }
    public int RemainingMovement { get; set; }

    public MovementComponent(int movement)
    {
        IsMoving = false;
        Movement = movement;
        RemainingMovement = movement;
    }
}