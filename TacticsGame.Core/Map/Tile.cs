using System.Drawing;

namespace TacticsGame.Core.Map;

public class Tile
{
    public Point Location { get; }
    public TileType Type { get; }

    public Tile(Point location, TileType type)
    {
        Location = location;
        Type = type;
    }
}

public enum TileType
{
    Field,
    Obstacle,
    Abyss
}