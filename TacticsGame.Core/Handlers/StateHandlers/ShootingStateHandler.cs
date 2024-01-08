using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Handlers.StateHandlers;

public class ShootingStateHandler : IStateHandler
{
    private bool _isShooting;

    public ShootingStateHandler(StateProvider stateProvider)
    {
        stateProvider.MovingStateChanged += HandleShootingStateChanged;
    }

    public bool GetState()
    {
        return _isShooting;
    }

    private void HandleShootingStateChanged(object sender, bool state)
    {
        _isShooting = state;
    }
}