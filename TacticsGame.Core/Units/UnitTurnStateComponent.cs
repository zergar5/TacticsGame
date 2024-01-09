namespace TacticsGame.Core.Units;

public struct UnitTurnStateComponent
{
    public bool MadeTurn { get; set; }

    public UnitTurnStateComponent()
    {
        MadeTurn = false;
    }
}