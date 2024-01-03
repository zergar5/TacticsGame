using System.Drawing;

namespace TacticsGame.Core.Map;

public struct BattlefieldComponent
{
    public Battlefield Map { get; }
    public SizeF TileSize { get; }

    public BattlefieldComponent(Battlefield map, SizeF tileSize)
    {
        Map = map;
        TileSize = tileSize;
    }
}