using Leopotam.EcsLite;
using System.Drawing;
using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Scene;

public class Cartographer
{
    private readonly EcsWorld _world;

    private readonly BattlefieldTiles _battlefieldTiles;
    private readonly SizeF _tileSize;
    private RectangleF _tileRectangle;
    private PointF _rectangleLocation;

    private PointF _bufferLocation;

    public Cartographer(EcsWorld world)
    {
        _world = world;

        var battlefields = _world.GetPool<BattlefieldComponent>();

        var battlefieldFilter = _world.Filter<BattlefieldComponent>().End();

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

    public (int row, int column) FindIndex(PointF location)
    {
        var row = FindRow(location);

        if (row == -1) return (-1, -1);

        var column = FindColumn(location, row);

        return column != -1 ? (row, column) : (-1, -1);
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
}