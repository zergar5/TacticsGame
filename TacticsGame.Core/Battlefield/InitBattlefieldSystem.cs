using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Context;
using TacticsGame.Core.Render;

namespace TacticsGame.Core.Battlefield;

public class InitBattlefieldSystem : IEcsInitSystem
{
    [EcsInject] private EntityBuilder _entityBuilder;
    [EcsInject] private IBattlefieldGenerator _battlefieldGenerator;

    public void Init(IEcsSystems systems)
    {
        CreateBattlefield();
    }

    private void CreateBattlefield()
    {
        var battlefield = _battlefieldGenerator.Generate();

        var battlefieldComponent = new BattlefieldComponent(battlefield, new SizeF(0.1f, 0.1f));

        _entityBuilder
            .Init()
            .Set(battlefieldComponent)
            .Set(new RenderComponent(RenderingType.Battlefield))
            .Build();
    }
}