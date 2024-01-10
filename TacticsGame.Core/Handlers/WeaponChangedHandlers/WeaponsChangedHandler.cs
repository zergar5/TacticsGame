using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Handlers.WeaponChangedHandlers;

public class WeaponsChangedHandler
{
    private int _weaponId;

    public WeaponsChangedHandler(CurrentWeaponIdProvider currentWeaponIdProvider)
    {
        currentWeaponIdProvider.WeaponChanged += HandleWeaponChanged;
    }

    public int GetId()
    {
        return _weaponId;
    }

    private void HandleWeaponChanged(object sender, int weaponId)
    {
        _weaponId = weaponId;
    }
}