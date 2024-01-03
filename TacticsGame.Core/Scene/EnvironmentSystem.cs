using System.Drawing;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using TacticsGame.Core.Map;

namespace TacticsGame.Core.Scene;

public class EnvironmentSystem : IEcsInitSystem
{
    [EcsInject] private BattlefieldGenerator.BattlefieldGenerator _battlefieldGenerator;
    [EcsInject] private EcsWorld _world;
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        CreateBattlefield();
    }

    private void CreateBattlefield()
    {
        var battlefield = _battlefieldGenerator.Generate();
        var comp = _world.GetPool<BattlefieldComponent>();
        comp.Add(0) = new BattlefieldComponent(battlefield, new SizeF(0.1f, 0.1f));
    }
}