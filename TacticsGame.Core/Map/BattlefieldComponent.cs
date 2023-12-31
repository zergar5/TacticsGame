using System.Drawing;

namespace TacticsGame.Core.Map;

public struct BattlefieldComponent
{
    public Battlefield Map { get; }
    public Size TileSize { get; }

    public BattlefieldComponent(Battlefield map, Size tileSize)
    {
        Map = map;
        TileSize = tileSize;
    }
}