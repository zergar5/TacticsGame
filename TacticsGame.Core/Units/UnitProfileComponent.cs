namespace TacticsGame.Core.Units;

public struct UnitProfileComponent
{
    public int Range { get; set; }
    public int Toughness { get; set; }
    public int Save { get; set; }
    public int Wounds { get; set; }

    public UnitProfileComponent(int range, int toughness, int save, int wounds)
    {
        Range = range;
        Toughness = toughness;
        Save = save;
        Wounds = wounds;
    }
}