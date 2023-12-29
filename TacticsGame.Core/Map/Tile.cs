using System.Drawing;

namespace TacticsGame.Core.Map;

public class Tile
{
    public Point Location { get; }
    public TileType Type { get; }
}

public enum TileType
{
    Free,
    Obstacle,
    Abyss
}