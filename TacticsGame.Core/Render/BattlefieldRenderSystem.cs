﻿using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Drawing;
using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Render;

public class BattlefieldRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private OpenGL _gl;

    private EcsFilter _battlefieldFilter;

    private EcsPool<BattlefieldComponent> _battlefields;
    private Texture _texture = new();

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        _battlefields = world.GetPool<BattlefieldComponent>();

        _gl.Enable(OpenGL.GL_TEXTURE_2D);

        var textureImage = new Bitmap(@$"{Directory.GetCurrentDirectory()}\TacticsGame\Assets\Textures\Map\bf1.jpg");

        _texture.Create(_gl, textureImage);

        //_texture.Bind(_gl);
    }

    public void Run(IEcsSystems systems)
    {
        //_gl.ActiveTexture(_texture.TextureName);
        _gl.BindTexture(OpenGL.GL_TEXTURE_2D, _texture.TextureName);
        
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

        //_gl.Disable(OpenGL.GL_TEXTURE_2D);

        _gl.LineWidth(2);

        RenderBattlefield();
    }

    private void RenderBattlefield()
    {
        foreach (var battlefield in _battlefieldFilter)
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
}