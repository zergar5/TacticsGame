namespace TacticsGame.Core.Dto;

public class UnitDto
{
    public int UnitId { get; set; }
    public int Wounds { get; set; }
    public int RemainingWounds { get; set; }
    public string Sprite { get; set; }
    public Dictionary<int, WeaponDto> WeaponsDtos { get; set; }

    public UnitDto(int unitId, int wounds, int remainingWounds, string sprite, Dictionary<int, WeaponDto> weaponsDtos)
    {
        UnitId = unitId;
        Wounds = wounds;
        RemainingWounds = remainingWounds;
        Sprite = sprite;
        WeaponsDtos = weaponsDtos;
    }
}