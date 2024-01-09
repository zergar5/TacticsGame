using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Providers;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Render;

public class UnitsRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;
    [EcsInject] private AssetsProvider _assetsProvider;

    private EcsFilter _units;
    private EcsPool<LocationComponent> _transforms;

    private SizeF _tileSize;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        var battlefieldFilter = world.Filter<BattlefieldComponent>().End();
        _units = world.Filter<UnitProfileComponent>().End();

        var battlefields = world.GetPool<BattlefieldComponent>();
        _transforms = world.GetPool<LocationComponent>();

        foreach (var battlefield in battlefieldFilter)
        {
            var battlefieldComponent = battlefields.Get(battlefield);

            _tileSize = battlefieldComponent.TileSize;
        }
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var unit in _units)
        {
            if(unit == 1) _gl.BindTexture(OpenGL.GL_TEXTURE_2D, _assetsProvider.GetTexture("NecronWarrior"));
            else if(unit == 2) _gl.BindTexture(OpenGL.GL_TEXTURE_2D, _assetsProvider.GetTexture("Termagant"));

            var location = _transforms.Get(unit).Location;

            _gl.Color(1f, 1f, 1f, 1f);
            _gl.Begin(OpenGL.GL_TRIANGLE_FAN);

            _gl.TexCoord(0f, 1f);
            _gl.Vertex(location.X - _tileSize.Width / 2, location.Y - _tileSize.Height / 2);
            _gl.TexCoord(1f, 1f);
            _gl.Vertex(location.X + _tileSize.Width / 2, location.Y - _tileSize.Height / 2);
            _gl.TexCoord(1f, 0f);
            _gl.Vertex(location.X + _tileSize.Width / 2, location.Y + _tileSize.Height / 2);
            _gl.TexCoord(0f, 0f);
            _gl.Vertex(location.X - _tileSize.Width / 2, location.Y + _tileSize.Height / 2);

            _gl.End();

            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
    }
}