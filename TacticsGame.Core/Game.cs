using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Render;
using TacticsGame.Core.Scene;

namespace TacticsGame.Core;

public class Game
{
    private readonly EcsWorld _world;

    private EcsSystems _environmentSystems;
    private EcsSystems _renderSystems;

    public Game()
    {
        _world = new EcsWorld();

        var entity = _world.NewEntity();

        _environmentSystems = new EcsSystems(_world);
        _environmentSystems
            .Add(new EnvironmentSystem())
            .Inject(new BattlefieldGenerator.BattlefieldGenerator(), _world)
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