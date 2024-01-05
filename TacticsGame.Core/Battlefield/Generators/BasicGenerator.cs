using System.Drawing;

namespace TacticsGame.Core.Battlefield.Generators;

public class BasicGenerator : IBattlefieldGenerator
{
    private BattlefieldTiles Init()
    {
        var tiles = new Tile[12, 13];

        var stepX = 0.1f;
        var stepY = 0.1f;

        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                tiles[i, j] = new Tile(
                    new PointF(-0.7f + stepX * j + stepX / 2, -0.7f + stepY * i + stepY / 2),
                    TileType.Field);
            }
        }

        return new BattlefieldTiles(new SizeF(1.1f, 1.4f), tiles);
    }

    public BattlefieldTiles Generate()
    {
        var battlefield = Init();

        GenerateWalls(battlefield);
        GenerateObstacles(battlefield);
        GenerateAbysses(battlefield);

        return battlefield;
    }

    private void GenerateWalls(BattlefieldTiles battlefield)
    {
        battlefield.Tiles[10, 0].Type = TileType.Wall;
        battlefield.Tiles[11, 0].Type = TileType.Wall;

        battlefield.Tiles[11, 1].Type = TileType.Wall;
        battlefield.Tiles[11, 2].Type = TileType.Wall;

        battlefield.Tiles[11, 9].Type = TileType.Wall;

        battlefield.Tiles[10, 10].Type = TileType.Wall;
        battlefield.Tiles[11, 10].Type = TileType.Wall;

        battlefield.Tiles[10, 11].Type = TileType.Wall;
        battlefield.Tiles[11, 11].Type = TileType.Wall;

        battlefield.Tiles[7, 12].Type = TileType.Wall;
        battlefield.Tiles[8, 12].Type = TileType.Wall;
        battlefield.Tiles[9, 12].Type = TileType.Wall;
        battlefield.Tiles[10, 12].Type = TileType.Wall;
        battlefield.Tiles[11, 12].Type = TileType.Wall;
    }

    private void GenerateObstacles(BattlefieldTiles battlefield)
    {
        battlefield.Tiles[9, 2].Type = TileType.Obstacle;
        battlefield.Tiles[9, 3].Type = TileType.Obstacle;

        battlefield.Tiles[9, 8].Type = TileType.Obstacle;

        battlefield.Tiles[3, 4].Type = TileType.Obstacle;
        battlefield.Tiles[3, 5].Type = TileType.Obstacle;
        battlefield.Tiles[3, 6].Type = TileType.Obstacle;
        battlefield.Tiles[3, 7].Type = TileType.Obstacle;
        battlefield.Tiles[3, 8].Type = TileType.Obstacle;
    }

    private void GenerateAbysses(BattlefieldTiles battlefield)
    {
        battlefield.Tiles[0, 0].Type = TileType.Abyss;
        battlefield.Tiles[1, 0].Type = TileType.Abyss;
        battlefield.Tiles[2, 0].Type = TileType.Abyss;
        battlefield.Tiles[3, 0].Type = TileType.Abyss;
        battlefield.Tiles[4, 0].Type = TileType.Abyss;

        battlefield.Tiles[0, 1].Type = TileType.Abyss;
        battlefield.Tiles[1, 1].Type = TileType.Abyss;
        battlefield.Tiles[2, 1].Type = TileType.Abyss;
        battlefield.Tiles[3, 1].Type = TileType.Abyss;
        battlefield.Tiles[4, 1].Type = TileType.Abyss;

        battlefield.Tiles[0, 2].Type = TileType.Abyss;
        battlefield.Tiles[1, 2].Type = TileType.Abyss;

        battlefield.Tiles[0, 3].Type = TileType.Abyss;

        battlefield.Tiles[0, 9].Type = TileType.Abyss;

        battlefield.Tiles[0, 10].Type = TileType.Abyss;
        battlefield.Tiles[1, 10].Type = TileType.Abyss;
        battlefield.Tiles[2, 10].Type = TileType.Abyss;

        battlefield.Tiles[0, 11].Type = TileType.Abyss;
        battlefield.Tiles[1, 11].Type = TileType.Abyss;
        battlefield.Tiles[2, 11].Type = TileType.Abyss;
        battlefield.Tiles[3, 11].Type = TileType.Abyss;

        battlefield.Tiles[0, 12].Type = TileType.Abyss;
        battlefield.Tiles[1, 12].Type = TileType.Abyss;
        battlefield.Tiles[2, 12].Type = TileType.Abyss;
        battlefield.Tiles[3, 12].Type = TileType.Abyss;
        battlefield.Tiles[4, 12].Type = TileType.Abyss;
    }
}