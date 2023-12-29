using System.Drawing;

namespace TacticsGame.Core.Map;

public class Map
{
    public Size Size { get; }
    public Tile[,] Tiles { get; }

    public Map(Size size, Tile[,] tiles)
    {
        Size = size;
        Tiles = tiles;
    }
}