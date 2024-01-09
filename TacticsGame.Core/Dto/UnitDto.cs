namespace TacticsGame.Core.Dto;

public class UnitDto
{
    public int UnitId { get; set; }
    public int Wounds { get; set; }
    public int RemainingWounds { get; set; }
    public string Sprite { get; set; }
    public Dictionary<int, WeaponDto> WeaponsDtos { get; set; }
}