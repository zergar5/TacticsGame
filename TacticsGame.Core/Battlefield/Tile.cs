using System.Drawing;

namespace TacticsGame.Core.Battlefield;

public class Tile
{
    public PointF Location { get; }
    public TileType Type { get; set; }

    public Tile(PointF location, TileType type)
    {
        Location = location;
        Type = type;
    }
}

public enum TileType
{
    Field,
    Wall,
    Obstacle,
    Abyss
}