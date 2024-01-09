using Leopotam.EcsLite;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Scene;

public class Cartographer
{
    private readonly EcsWorld _world;

    private readonly EcsFilter _currentUnit;
    private readonly EcsFilter _unitsFilter;

    private readonly EcsPool<UnitProfileComponent> _units;
    private readonly EcsPool<LocationComponent> _locations;
    private readonly EcsPool<OwnershipComponent> _ownerships;

    private readonly BattlefieldTiles _battlefieldTiles;
    private readonly SizeF _tileSize;
    private RectangleF _tileRectangle;
    private PointF _rectangleLocation;

    private PointF _bufferLocation;

    private readonly float _epsF = 1e-7f;

    public Cartographer(EcsWorld world)
    {
        _world = world;

        var battlefieldFilter = _world.Filter<BattlefieldComponent>().End();
        _currentUnit = _world.Filter<UnitProfileComponent>().Inc<CurrentUnitMarker>().End();
        _unitsFilter = _world.Filter<UnitProfileComponent>().Exc<CurrentUnitMarker>().End();

        var battlefields = _world.GetPool<BattlefieldComponent>();
        _units = _world.GetPool<UnitProfileComponent>();
        _locations = _world.GetPool<LocationComponent>();
        _ownerships = _world.GetPool<OwnershipComponent>();

        foreach (var battlefield in battlefieldFilter)
        {
            var battlefieldComponent = battlefields.Get(battlefield);

            _battlefieldTiles = battlefieldComponent.Map;
            _tileSize = battlefieldComponent.TileSize;
        }

        _tileRectangle = new RectangleF
        {
            Size = _tileSize
        };
    }

    public (int row, int column) FindTileIndex(PointF location)
    {
        var row = FindRow(location);

        if (row == -1) return (-1, -1);

        var column = FindColumn(location, row);

        return column != -1 ? (row, column) : (-1, -1);
    }

    public List<Tile> FindEnemyUnitTilesInRange(int range, int playerId)
    {
        (int currentRow, int currentColumn) currentIndex = (0, 0);

        foreach (var unit in _currentUnit)
        {
            currentIndex = FindTileIndex(_locations.Get(unit).Location);
        }

        var unitTiles = new List<Tile>();

        foreach (var unit in _unitsFilter)
        {
            if (_ownerships.Get(unit).OwnerId == playerId) continue;

            var tileIndex = FindTileIndex(_locations.Get(unit).Location);

            var distance = CalculateDistanceInTiles(currentIndex, tileIndex);

            if (distance > 1 && distance <= range) unitTiles.Add(_battlefieldTiles[tileIndex]);
        }

        return unitTiles;
    }

    public bool CheckTileForUnit(Tile tile)
    {
        foreach (var unit in _unitsFilter)
        {
            var location = _locations.Get(unit).Location;

            if(CompareLocations(tile.Location, location)) return true;
        }

        return false;
    }

    public int FindUnitId(Tile tile)
    {
        foreach (var unit in _unitsFilter)
        {
            var location = _locations.Get(unit).Location;

            if (CompareLocations(tile.Location, location)) return unit;
        }

        return -1;
    }

    private int FindRow(PointF location)
    {
        for (var i = 0; i < _battlefieldTiles.CountRows; i++)
        {
            var tileLocation = _battlefieldTiles[i, 0].Location;

            _bufferLocation.X = tileLocation.X;
            _bufferLocation.Y = location.Y;

            MakeRectangle(tileLocation);

            _tileRectangle.Location = _rectangleLocation;

            if (_tileRectangle.Contains(_bufferLocation)) return i;
        }

        return -1;
    }

    private int FindColumn(PointF location, int row)
    {
        for (var j = 0; j < _battlefieldTiles.CountColumns; j++)
        {
            var tileLocation = _battlefieldTiles[row, j].Location;

            MakeRectangle(tileLocation);

            _tileRectangle.Location = _rectangleLocation;

            if (_tileRectangle.Contains(location)) return j;
        }

        return -1;
    }

    private void MakeRectangle(PointF tileLocation)
    {
        _rectangleLocation.X = tileLocation.X - _tileSize.Width / 2;
        _rectangleLocation.Y = tileLocation.Y - _tileSize.Height / 2;
    }

    private int CalculateDistanceInTiles((int currentRow, int currentColumn) currentTile, (int row, int column) tile)
    {
        return Math.Abs(tile.row - currentTile.currentRow) + Math.Abs(tile.column - currentTile.currentColumn);
    }

    private bool CompareLocations(PointF location1, PointF location2)
    {
        return Math.Abs(location1.X - location2.X) <= _epsF && Math.Abs(location1.Y - location2.Y) <= _epsF;
    }
}