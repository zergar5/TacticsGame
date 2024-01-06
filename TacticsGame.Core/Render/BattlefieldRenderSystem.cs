using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Render;

public class BattlefieldRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;

    private EcsFilter _currentUnitFilter;

    private EcsPool<BattlefieldComponent> _battlefields;
    private EcsPool<ReachableTilesComponent> _reachableTiles;
    private Texture _texture;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().End();

        _battlefields = world.GetPool<BattlefieldComponent>();
        _reachableTiles = world.GetPool<ReachableTilesComponent>();

        _gl.Disable(OpenGL.GL_DEPTH_TEST);

        _gl.Enable(OpenGL.GL_BLEND);
        _gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

        var textureImage = new Bitmap(@"..\TacticsGame.Core\Textures\Map\bf1.jpg");

        var texture = new Texture();

        texture.Create(_gl, textureImage);

        texture.Bind(_gl);
    }

    public void Run(IEcsSystems systems)
    {
        _gl.ClearColor(0f, 0f, 0f, 1f);
        _gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT/* | OpenGL.GL_DEPTH_BUFFER_BIT*/);

        //_gl.ActiveTexture(OpenGL.GL_TEXTURE0);
        //_gl.BindTexture(OpenGL.GL_TEXTURE_2D, _assetManager.GetTexture("Floor"));
        _gl.Enable(OpenGL.GL_TEXTURE_2D);

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

        _gl.Disable(OpenGL.GL_TEXTURE_2D);

        RenderReachableTiles();
        RenderBattlefield();
    }

    private void RenderBattlefield()
    {
        var component = _battlefields.Get(0);

        var tiles = component.Map;
        var tileSize = component.TileSize;

        _gl.LineWidth(2);

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

    private void RenderReachableTiles()
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            var reachableTiles = _reachableTiles.Get(currentUnit).ReachableTiles;

            var tileSize = new SizeF(0.5f, 0.5f);

            foreach (var tile in reachableTiles)
            {
                _gl.Begin(OpenGL.GL_TRIANGLE_FAN);

                _gl.Color(0.5f, 0.5f, 0.5f, 0.95f);

                _gl.Vertex(tile.Location.X - tileSize.Width / 2, tile.Location.Y - tileSize.Height / 2);
                _gl.Vertex(tile.Location.X + tileSize.Width / 2, tile.Location.Y - tileSize.Height / 2);
                _gl.Vertex(tile.Location.X + tileSize.Width / 2, tile.Location.Y + tileSize.Height / 2);
                _gl.Vertex(tile.Location.X - tileSize.Width / 2, tile.Location.Y + tileSize.Height / 2);

                _gl.End();
            }
        }
    }


}