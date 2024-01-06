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

        return new BattlefieldTiles(new SizeF(7f, 15f), tiles);
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
        //battlefield[10, 0].Type = TileType.Wall;
        //battlefield[11, 0].Type = TileType.Wall;

        //battlefield[11, 1].Type = TileType.Wall;
        //battlefield[11, 2].Type = TileType.Wall;

        //battlefield[11, 9].Type = TileType.Wall;

        //battlefield[10, 10].Type = TileType.Wall;
        //battlefield[11, 10].Type = TileType.Wall;

        //battlefield[10, 11].Type = TileType.Wall;
        //battlefield[11, 11].Type = TileType.Wall;

        //battlefield[7, 12].Type = TileType.Wall;
        //battlefield[8, 12].Type = TileType.Wall;
        //battlefield[9, 12].Type = TileType.Wall;
        //battlefield[10, 12].Type = TileType.Wall;
        //battlefield[11, 12].Type = TileType.Wall;
    }

    private void GenerateObstacles(BattlefieldTiles battlefield)
    {
        //battlefield[9, 2].Type = TileType.Obstacle;
        //battlefield[9, 3].Type = TileType.Obstacle;

        //battlefield[9, 8].Type = TileType.Obstacle;

        //battlefield[3, 4].Type = TileType.Obstacle;
        //battlefield[3, 5].Type = TileType.Obstacle;
        //battlefield[3, 6].Type = TileType.Obstacle;
        //battlefield[3, 7].Type = TileType.Obstacle;
        //battlefield[3, 8].Type = TileType.Obstacle;
    }

    private void GenerateAbysses(BattlefieldTiles battlefield)
    {
        //battlefield[0, 0].Type = TileType.Abyss;
        //battlefield[1, 0].Type = TileType.Abyss;
        //battlefield[2, 0].Type = TileType.Abyss;
        //battlefield[3, 0].Type = TileType.Abyss;
        //battlefield[4, 0].Type = TileType.Abyss;

        //battlefield[0, 1].Type = TileType.Abyss;
        //battlefield[1, 1].Type = TileType.Abyss;
        //battlefield[2, 1].Type = TileType.Abyss;
        //battlefield[3, 1].Type = TileType.Abyss;
        //battlefield[4, 1].Type = TileType.Abyss;

        //battlefield[0, 2].Type = TileType.Abyss;
        //battlefield[1, 2].Type = TileType.Abyss;

        //battlefield[0, 3].Type = TileType.Abyss;

        //battlefield[0, 9].Type = TileType.Abyss;

        //battlefield[0, 10].Type = TileType.Abyss;
        //battlefield[1, 10].Type = TileType.Abyss;
        //battlefield[2, 10].Type = TileType.Abyss;

        //battlefield[0, 11].Type = TileType.Abyss;
        //battlefield[1, 11].Type = TileType.Abyss;
        //battlefield[2, 11].Type = TileType.Abyss;
        //battlefield[3, 11].Type = TileType.Abyss;

        //battlefield[0, 12].Type = TileType.Abyss;
        //battlefield[1, 12].Type = TileType.Abyss;
        //battlefield[2, 12].Type = TileType.Abyss;
        //battlefield[3, 12].Type = TileType.Abyss;
        //battlefield[4, 12].Type = TileType.Abyss;
    }
}