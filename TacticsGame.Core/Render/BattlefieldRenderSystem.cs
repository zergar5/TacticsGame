using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Render;

public class BattlefieldRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;
    [EcsInject] private AssetsProvider _assetsProvider;

    private EcsFilter _battlefieldFilter;

    private EcsPool<BattlefieldComponent> _battlefields;
    private EcsPool<SpriteComponent> _sprites;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        _battlefields = world.GetPool<BattlefieldComponent>();
        _sprites = world.GetPool<SpriteComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var battlefield in _battlefieldFilter)
        {
            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, _assetsProvider.GetTexture(_sprites.Get(battlefield).Sprite));

            _gl.Color(1f, 1f, 1f, 1f);
            _gl.Begin(OpenGL.GL_TRIANGLE_FAN);

            _gl.TexCoord(0f, 1f);
            _gl.Vertex(-8f, -4.5f);
            _gl.TexCoord(1f, 1f);
            _gl.Vertex(8f, -4.5f);
            _gl.TexCoord(1f, 0f);
            _gl.Vertex(8f, 4.5f);
            _gl.TexCoord(0f, 0f);
            _gl.Vertex(-8f, 4.5f);

            _gl.End();

            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            RenderBattlefield(battlefield);
        }
    }

    private void RenderBattlefield(int battlefield)
    {
        var battlefieldComponent = _battlefields.Get(battlefield);

        var tiles = battlefieldComponent.Map;
        var tileSize = battlefieldComponent.TileSize;

        for (var i = 0; i < tiles.CountRows; i++)
        {
            for (var j = 0; j < tiles.CountColumns; j++)
            {
                if (tiles[i, j].Type != TileType.Field) continue;

                var tileLocation = tiles[i, j].Location;

                _gl.Begin(OpenGL.GL_LINE_LOOP);

                _gl.Color(0f, 1f, 0f, 1f);

                _gl.Vertex(tileLocation.X - tileSize.Width / 2, tileLocation.Y - tileSize.Height / 2);
                _gl.Vertex(tileLocation.X + tileSize.Width / 2, tileLocation.Y - tileSize.Height / 2);
                _gl.Vertex(tileLocation.X + tileSize.Width / 2, tileLocation.Y + tileSize.Height / 2);
                _gl.Vertex(tileLocation.X - tileSize.Width / 2, tileLocation.Y + tileSize.Height / 2);

                _gl.End();
            }
        }
    }
}