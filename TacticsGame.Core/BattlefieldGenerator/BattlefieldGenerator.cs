using System.Drawing;
using TacticsGame.Core.Map;

namespace TacticsGame.Core.BattlefieldGenerator;

public class BattlefieldGenerator
{
    private Battlefield Init()
    {
        var tiles = new Tile[35, 75];

        var stepX = 350 / 35;
        var stepY = 750 / 75;

        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
               tiles[i, j] = new Tile(
                   new Point(25 + stepX * (j + 1) - stepX / 2, 50 + stepY * (i + 1) - stepY / 2), 
                   TileType.Field);
            }
        }

        var tile = tiles[34, 74];

        return new Battlefield(new Size(750, 350), tiles);
    }

    public Battlefield Generate()
    {
        var battlefield = Init();

        GenerateObstacles(battlefield);
        GenerateAbysses(battlefield);

        return battlefield;
    }

    private void GenerateObstacles(Battlefield battlefield)
    {

    }

    private void GenerateAbysses(Battlefield battlefield)
    {

    }
}