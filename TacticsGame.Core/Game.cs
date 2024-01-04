using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Context;
using TacticsGame.Core.Render;
using TacticsGame.Core.Scene;

namespace TacticsGame.Core;

public class Game
{
    private readonly EcsWorld _world;

    private readonly EntityBuilder _entityBuilder;

    private readonly EcsSystems _setupSystems;
    private EcsSystems _environmentSystems;
    private EcsSystems _renderSystems;

    public Game()
    {
        _world = new EcsWorld();
        _entityBuilder = new EntityBuilder(_world);

        _world.NewEntity();

        _setupSystems = new EcsSystems(_world);
        _setupSystems
            .Add(new EnvironmentSystem())
            .Inject(new BattlefieldGenerator.BattlefieldGenerator())
            .Init();
    }

    public void InitRenderSystems(OpenGL gl)
    {
        _renderSystems = new EcsSystems(_world);
        _renderSystems
            .Add(new BattlefieldRenderSystem())
            .Inject(gl)
            .Init();
    }

    public void Render()
    {
        _renderSystems.Run();
    }
}