using System.Drawing;

namespace TacticsGame.Core.Battlefield;

public class BattlefieldTiles
{
    //Возможно стоит заменить на бинарное дерево
    private readonly Tile[,] _tiles;
    public SizeF Size { get; }

    public int CountRows => _tiles.GetLength(0);
    public int CountColumns => _tiles.GetLength(1);

    public BattlefieldTiles(SizeF size, Tile[,] tiles)
    {
        Size = size;
        _tiles = tiles;
    }

    public Tile this[int i, int j]
    {
        get => _tiles[i, j];
        set => _tiles[i, j] = value;
    }

    public Tile this[(int i, int j) ij]
    {
        get => this[ij.i, ij.j];
        set => this[ij.i, ij.j] = value;
    }
}