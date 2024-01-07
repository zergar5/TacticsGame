using System.Drawing;
using Leopotam.EcsLite;

namespace TacticsGame.Core.Battlefield;

public class Cartographer
{
    private readonly EcsWorld _world;

    private readonly BattlefieldTiles _battlefieldTiles;
    private readonly SizeF _tileSize;
    private RectangleF _tileRectangle;
    private PointF _tileLocation;

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
        for (var i = 5; i < 7; i++)
        {
            for (var j = 0; j < _battlefieldTiles.CountColumns; j++)
            {
                var tileLocation = _battlefieldTiles[i, j].Location;

                _tileLocation.X = tileLocation.X - _tileSize.Width / 2;
                _tileLocation.Y = tileLocation.Y - _tileSize.Height / 2;

                _tileRectangle.Location = _tileLocation;

                if(_tileRectangle.Contains(location)) return (i, j);
            }
        }

        return (-1, -1);
    } 
}