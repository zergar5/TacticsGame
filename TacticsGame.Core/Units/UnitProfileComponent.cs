using TacticsGame.Core.Units.Datasheets;

namespace TacticsGame.Core.Units;

public struct UnitProfileComponent
{
    public int Movement { get; set; }
    public int Toughness { get; set; }
    public int Save { get; set; }
    public int Wounds { get; set; }

    public UnitProfileComponent(int movement, int toughness, int save, int wounds)
    {
        Movement = movement;
        Toughness = toughness;
        Save = save;
        Wounds = wounds;
    }

    public UnitProfileComponent(UnitDatasheet unitDatasheet)
    {
        Movement = unitDatasheet.Movement;
        Toughness = unitDatasheet.Toughness;
        Save = unitDatasheet.Save;
        Wounds = unitDatasheet.Wounds;
    }
}