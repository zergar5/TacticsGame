namespace TacticsGame.Core.Hp;

public struct HpComponent
{
    public int Wounds { get; set; }
    public int RemainingWounds { get; set; }

    public HpComponent(int wounds, int remainingWounds)
    {
        Wounds = wounds;
        RemainingWounds = remainingWounds;
    }
}