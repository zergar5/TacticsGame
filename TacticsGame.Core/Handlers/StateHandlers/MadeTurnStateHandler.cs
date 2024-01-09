using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Handlers.StateHandlers;

public class MadeTurnStateHandler : IStateHandler
{
    private bool _isMadeTurn;

    public MadeTurnStateHandler(StateProvider stateProvider)
    {
        stateProvider.MadeTurnStateChanged += HandleMadeTurnStateChanged;
    }

    public bool GetState()
    {
        return _isMadeTurn;
    }

    private void HandleMadeTurnStateChanged(object sender, bool state)
    {
        _isMadeTurn = state;
    }
}