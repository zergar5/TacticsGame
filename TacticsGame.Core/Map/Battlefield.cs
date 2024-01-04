using System.Drawing;

namespace TacticsGame.Core.Map;

public class Battlefield
{
    public SizeF Size { get; }
    //Возможно стоит заменить на бинарное дерево
    public Tile[,] Tiles { get; }

    public Battlefield(SizeF size, Tile[,] tiles)
    {
        Size = size;
        Tiles = tiles;
    }
}