using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;

namespace TacticsGame.Core.Scene;

public class EnvironmentSystem : IEcsInitSystem
{
    [EcsInject] private BattlefieldGenerator.BattlefieldGenerator _battlefieldGenerator;
    public void Init(IEcsSystems systems)
    {
        CreateBattlefield();
    }

    public void CreateBattlefield()
    {
        var battlefield = _battlefieldGenerator.Generate();
    }
}