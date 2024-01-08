using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;

namespace TacticsGame.Core.Render;

public class InitRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;

    public void Init(IEcsSystems systems)
    {
        _gl.Disable(OpenGL.GL_DEPTH_TEST);

        _gl.Enable(OpenGL.GL_BLEND);
        _gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
    }

    public void Run(IEcsSystems systems)
    {
        _gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
        _gl.ClearColor(0f, 0f, 0f, 1f);
    }
}