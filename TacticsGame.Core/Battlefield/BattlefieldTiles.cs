using System.Drawing;

namespace TacticsGame.Core.Battlefield;

public class BattlefieldTiles
{
    public SizeF Size { get; }
    //Возможно стоит заменить на бинарное дерево
    public Tile[,] Tiles { get; }

    public BattlefieldTiles(SizeF size, Tile[,] tiles)
    {
        Size = size;
        Tiles = tiles;
    }
}