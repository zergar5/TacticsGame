using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Render;

namespace TacticsGame.Core;

public class Game
{
    private readonly EcsWorld _world;

    private EcsSystems _renderSystems;

    public Game()
    {
        _world = new EcsWorld();
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