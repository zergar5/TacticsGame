namespace TacticsGame.Core.Dto;

public class WeaponDto
{
    public int WeaponId { get; set; }
    public string Sprite { get; set; }

    public WeaponDto(int weaponId, string sprite)
    {
        WeaponId = weaponId;
        Sprite = sprite;
    }
}