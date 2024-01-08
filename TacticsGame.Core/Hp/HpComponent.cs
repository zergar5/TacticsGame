namespace TacticsGame.Core.Hp;

public struct HPComponent
{
    public int Wounds { get; set; }
    public int RemainingWounds { get; set; }

    public HPComponent(int wounds)
    {
        Wounds = wounds;
        RemainingWounds = wounds;
    }
}