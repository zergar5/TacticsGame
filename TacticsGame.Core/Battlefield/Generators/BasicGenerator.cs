using System.Drawing;

namespace TacticsGame.Core.Battlefield.Generators;

public class BasicGenerator : IBattlefieldGenerator
{
    private BattlefieldTiles Init()
    {
        var tiles = new Tile[11, 20];

        var stepX = 0.5f;
        var stepY = 0.5f;

        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                tiles[i, j] = new Tile(
                    new PointF(-5.5f + stepX * j + stepX / 2, -3f + stepY * i + stepY / 2),
                    TileType.Field);
            }
        }

        return new BattlefieldTiles(new SizeF(10f, 5.5f), tiles);
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
        battlefield[7, 0].Type = TileType.Wall;
        battlefield[8, 0].Type = TileType.Wall;
        battlefield[9, 0].Type = TileType.Wall;
        battlefield[10, 0].Type = TileType.Wall;

        battlefield[8, 1].Type = TileType.Wall;
        battlefield[9, 1].Type = TileType.Wall;
        battlefield[10, 1].Type = TileType.Wall;

        battlefield[9, 2].Type = TileType.Wall;
        battlefield[10, 2].Type = TileType.Wall;

        battlefield[9, 3].Type = TileType.Wall;
        battlefield[10, 3].Type = TileType.Wall;

        battlefield[10, 4].Type = TileType.Wall;

        battlefield[10, 5].Type = TileType.Wall;

        battlefield[10, 6].Type = TileType.Wall;

        battlefield[10, 13].Type = TileType.Wall;

        battlefield[10, 14].Type = TileType.Wall;

        battlefield[9, 15].Type = TileType.Wall;
        battlefield[10, 15].Type = TileType.Wall;

        battlefield[9, 16].Type = TileType.Wall;
        battlefield[10, 16].Type = TileType.Wall;

        battlefield[8, 17].Type = TileType.Wall;
        battlefield[9, 17].Type = TileType.Wall;
        battlefield[10, 17].Type = TileType.Wall;

        battlefield[8, 18].Type = TileType.Wall;
        battlefield[9, 18].Type = TileType.Wall;
        battlefield[10, 18].Type = TileType.Wall;

        battlefield[6, 19].Type = TileType.Wall;
        battlefield[7, 19].Type = TileType.Wall;
        battlefield[8, 19].Type = TileType.Wall;
        battlefield[9, 19].Type = TileType.Wall;
        battlefield[10, 19].Type = TileType.Wall;
    }

    private void GenerateObstacles(BattlefieldTiles battlefield)
    {
        battlefield[7, 4].Type = TileType.Obstacle;
        battlefield[8, 4].Type = TileType.Obstacle;

        battlefield[7, 5].Type = TileType.Obstacle;
        battlefield[8, 5].Type = TileType.Obstacle;

        battlefield[7, 13].Type = TileType.Obstacle;
        battlefield[8, 13].Type = TileType.Obstacle;

        battlefield[7, 14].Type = TileType.Obstacle;
        battlefield[8, 14].Type = TileType.Obstacle;

        battlefield[2, 7].Type = TileType.Obstacle;
        battlefield[2, 8].Type = TileType.Obstacle;
        battlefield[2, 10].Type = TileType.Obstacle;
        battlefield[2, 11].Type = TileType.Obstacle;
        battlefield[2, 12].Type = TileType.Obstacle;

        battlefield[3, 6].Type = TileType.Obstacle;
        battlefield[3, 7].Type = TileType.Obstacle;
        battlefield[3, 8].Type = TileType.Obstacle;
        battlefield[3, 9].Type = TileType.Obstacle;
        battlefield[3, 10].Type = TileType.Obstacle;
        battlefield[3, 11].Type = TileType.Obstacle;
        battlefield[3, 12].Type = TileType.Obstacle;
        battlefield[3, 13].Type = TileType.Obstacle;
    }

    private void GenerateAbysses(BattlefieldTiles battlefield)
    {
        battlefield[0, 0].Type = TileType.Abyss;
        battlefield[1, 0].Type = TileType.Abyss;
        battlefield[2, 0].Type = TileType.Abyss;
        battlefield[3, 0].Type = TileType.Abyss;

        battlefield[0, 1].Type = TileType.Abyss;
        battlefield[1, 1].Type = TileType.Abyss;
        battlefield[2, 1].Type = TileType.Abyss;
        battlefield[3, 1].Type = TileType.Abyss;

        battlefield[0, 2].Type = TileType.Abyss;
        battlefield[1, 2].Type = TileType.Abyss;
        battlefield[2, 2].Type = TileType.Abyss;

        battlefield[0, 3].Type = TileType.Abyss;
        battlefield[1, 3].Type = TileType.Abyss;

        battlefield[0, 4].Type = TileType.Abyss;

        battlefield[0, 5].Type = TileType.Abyss;

        battlefield[0, 15].Type = TileType.Abyss;

        battlefield[0, 16].Type = TileType.Abyss;
        battlefield[1, 16].Type = TileType.Abyss;

        battlefield[0, 17].Type = TileType.Abyss;
        battlefield[1, 17].Type = TileType.Abyss;
        battlefield[2, 17].Type = TileType.Abyss;

        battlefield[0, 18].Type = TileType.Abyss;
        battlefield[1, 18].Type = TileType.Abyss;
        battlefield[2, 18].Type = TileType.Abyss;

        battlefield[0, 19].Type = TileType.Abyss;
        battlefield[1, 19].Type = TileType.Abyss;
        battlefield[2, 19].Type = TileType.Abyss;
        battlefield[3, 19].Type = TileType.Abyss;
    }
}