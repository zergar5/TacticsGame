using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Scene;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Render;

public class UIRenderSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly OpenGL _gl;
    [EcsInject] private readonly Cartographer _cartographer;
    [EcsInject] private readonly MousePositionHandler _positionHandler;

    private EcsFilter _currentUnitFilter;
    private EcsFilter _battlefieldFilter;

    private EcsPool<BattlefieldComponent> _battlefield;
    private EcsPool<ReachableTilesComponent> _reachableTiles;
    private EcsPool<LocationComponent> _locations;

    private BattlefieldTiles _battlefieldTiles;
    private SizeF _tileSize;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().End();
        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        _battlefield = world.GetPool<BattlefieldComponent>();
        _reachableTiles = world.GetPool<ReachableTilesComponent>();
        _locations = world.GetPool<LocationComponent>();

        foreach (var battlefield in _battlefieldFilter)
        {
            var battlefieldComponent = _battlefield.Get(battlefield);

            _battlefieldTiles = battlefieldComponent.Map;
            _tileSize = battlefieldComponent.TileSize;
        }
    }

    public void Run(IEcsSystems systems)
    {
        RenderCurrentUnitTile();
        RenderMouseTile();
        RenderReachableTiles();
    }

    private void RenderCurrentUnitTile()
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            var positionIndex = _cartographer.FindTileIndex(_locations.Get(currentUnit).Location);

            if (positionIndex.row == -1 || positionIndex.column == -1) continue;

            var unitTile = _battlefieldTiles[positionIndex];

            _gl.Begin(OpenGL.GL_LINE_LOOP);

            _gl.Color(64/255f, 224/255f, 208/255f, 1f);

            _gl.Vertex(unitTile.Location.X - _tileSize.Width / 2, unitTile.Location.Y - _tileSize.Height / 2);
            _gl.Vertex(unitTile.Location.X + _tileSize.Width / 2, unitTile.Location.Y - _tileSize.Height / 2);
            _gl.Vertex(unitTile.Location.X + _tileSize.Width / 2, unitTile.Location.Y + _tileSize.Height / 2);
            _gl.Vertex(unitTile.Location.X - _tileSize.Width / 2, unitTile.Location.Y + _tileSize.Height / 2);

            _gl.End();
        }
    }

    private void RenderMouseTile()
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            var reachableTiles = _reachableTiles.Get(currentUnit).ReachableTiles;

            var positionIndex = _cartographer.FindTileIndex(_positionHandler.GetPosition());

            if (positionIndex.row == -1 || positionIndex.column == -1) continue;

            var mouseTile = _battlefieldTiles[positionIndex];

            if (!reachableTiles.Contains(mouseTile)) continue;

            _gl.Begin(OpenGL.GL_TRIANGLE_FAN);

            _gl.Color(0f, 0f, 0f, 0.4f);

            _gl.Vertex(mouseTile.Location.X - _tileSize.Width / 2, mouseTile.Location.Y - _tileSize.Height / 2);
            _gl.Vertex(mouseTile.Location.X + _tileSize.Width / 2, mouseTile.Location.Y - _tileSize.Height / 2);
            _gl.Vertex(mouseTile.Location.X + _tileSize.Width / 2, mouseTile.Location.Y + _tileSize.Height / 2);
            _gl.Vertex(mouseTile.Location.X - _tileSize.Width / 2, mouseTile.Location.Y + _tileSize.Height / 2);

            _gl.End();
        }
    }

    private void RenderReachableTiles()
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            var reachableTiles = _reachableTiles.Get(currentUnit).ReachableTiles;

            foreach (var tile in reachableTiles)
            {
                _gl.Begin(OpenGL.GL_TRIANGLE_FAN);

                _gl.Color(0.45f, 0.45f, 0.45f, 0.4f);

                _gl.Vertex(tile.Location.X - _tileSize.Width / 2, tile.Location.Y - _tileSize.Height / 2);
                _gl.Vertex(tile.Location.X + _tileSize.Width / 2, tile.Location.Y - _tileSize.Height / 2);
                _gl.Vertex(tile.Location.X + _tileSize.Width / 2, tile.Location.Y + _tileSize.Height / 2);
                _gl.Vertex(tile.Location.X - _tileSize.Width / 2, tile.Location.Y + _tileSize.Height / 2);

                _gl.End();
            }
        }
    }
}