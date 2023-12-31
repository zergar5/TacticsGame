using System.Drawing;

namespace TacticsGame.Core.Map;

public class Battlefield
{
    public Size Size { get; }
    public Tile[,] Tiles { get; }

    public Battlefield(Size size, Tile[,] tiles)
    {
        Size = size;
        Tiles = tiles;
    }
}