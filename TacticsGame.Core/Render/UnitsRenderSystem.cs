using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Render;

public class UnitsRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;

    private EcsFilter _units;
    private EcsPool<TransformComponent> _transforms;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _units = world.Filter<UnitProfileComponent>().End();

        _transforms = world.GetPool<TransformComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        _gl.PointSize(20);

        foreach (var unit in _units)
        {
            var location = _transforms.Get(unit).Location;

            _gl.Color(1f, 1f, 1f, 1f);
            _gl.Begin(OpenGL.GL_POINTS);

            _gl.Vertex(location.X, location.Y);

            _gl.End();
        }
    }
}