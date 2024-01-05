using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Context;
using TacticsGame.Core.Render;
using TacticsGame.Core.Units;

namespace TacticsGame.Core;

public class Game
{
    private readonly EcsWorld _world;

    private readonly EntityBuilder _entityBuilder;

    private readonly EcsSystems _setupSystems;
    private readonly EcsSystems _gameplaySystems;
    private EcsSystems _renderSystems;

    public Game()
    {
        _world = new EcsWorld();
        _entityBuilder = new EntityBuilder(_world);

        _setupSystems = new EcsSystems(_world);
        _setupSystems
            .Add(new InitBattlefieldSystem())
            .Add(new UnitSpawnerSystem())
            .Inject(_entityBuilder, new BasicGenerator())
            .Init();

        _gameplaySystems = new EcsSystems(_world);
        _gameplaySystems
            .Add()
            .Inject(_entityBuilder, new BasicGenerator())
            .Init();
    }

    public void InitRenderSystems(OpenGL gl)
    {
        _renderSystems = new EcsSystems(_world);
        _renderSystems
            .Add(new BattlefieldRenderSystem())
            .Add(new UnitsRenderSystem())
            .Inject(gl)
            .Init();
    }

    public void Render()
    {
        _renderSystems.Run();
    }
}