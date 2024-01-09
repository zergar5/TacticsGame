using System.Drawing;

namespace TacticsGame.Core.Providers;

public class CurrentWeaponIdProvider
{
    private int _weaponId;

    public int WeaponId
    {
        get => _weaponId;
        set
        {
            _weaponId = value;
            OnWeaponChanged(value);
        }
    }

    public event EventHandler<int> WeaponChanged;

    protected virtual void OnWeaponChanged(int value)
    {
        WeaponChanged?.Invoke(this, value);
    }
}