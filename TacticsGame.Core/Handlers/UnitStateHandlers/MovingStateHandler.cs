using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Handlers.UnitStateHandlers;

public class MovingStateHandler : IUnitStateHandler
{
    private bool _isMoving;

    public MovingStateHandler(UnitStateProvider unitStateProvider)
    {
        unitStateProvider.MovingStateChanged += HandleMovingStateChanged;
    }

    public bool GetState()
    {
        return _isMoving;
    }

    private void HandleMovingStateChanged(object sender, bool state)
    {
        _isMoving = state;
    }
}