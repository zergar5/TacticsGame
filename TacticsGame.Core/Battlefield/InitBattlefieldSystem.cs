using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Context;

namespace TacticsGame.Core.Battlefield;

public class InitBattlefieldSystem : IEcsInitSystem
{
    [EcsInject] private readonly EntityBuilder _entityBuilder;
    [EcsInject] private readonly IBattlefieldGenerator _battlefieldGenerator;

    public void Init(IEcsSystems systems)
    {
        CreateBattlefield();
    }

    private void CreateBattlefield()
    {
        var battlefield = _battlefieldGenerator.Generate();

        var battlefieldComponent = new BattlefieldComponent(battlefield, new SizeF(0.5f, 0.5f));

        _entityBuilder
            .Init()
            .Set(battlefieldComponent)
            .Build();
    }
}