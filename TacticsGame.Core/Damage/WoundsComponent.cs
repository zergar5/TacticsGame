namespace TacticsGame.Core.Damage;

public struct WoundsComponent
{
    public int Wounds { get; set; }
    public int RemainingWounds { get; set; }

    public WoundsComponent(int wounds)
    {
        Wounds = wounds;
        RemainingWounds = wounds;
    }
}