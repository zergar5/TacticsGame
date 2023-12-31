using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Map;

namespace TacticsGame.Core.Render;

public class BattlefieldRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;

    private EcsPool<BattlefieldComponent> _battlefields;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _battlefields = world.GetPool<BattlefieldComponent>();

        _gl.Enable(OpenGL.GL_TEXTURE_2D);

        _gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
        _gl.Enable(OpenGL.GL_BLEND);
    }

    public void Run(IEcsSystems systems)
    {
        throw new NotImplementedException();
    }

    private void RenderBattlefield()
    {

    }

    private void RenderBackground()
    {

    }
}