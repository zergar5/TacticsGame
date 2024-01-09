using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Handlers.StateHandlers;

public class MovingStateHandler : IStateHandler
{
    private bool _isMoving;

    public MovingStateHandler(StateProvider unitStateProvider)
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