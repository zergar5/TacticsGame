using System.Drawing;

namespace TacticsGame.Core.Battlefield;

public struct BattlefieldComponent
{
    public BattlefieldTiles Map { get; }
    public SizeF TileSize { get; }

    public BattlefieldComponent(BattlefieldTiles map, SizeF tileSize)
    {
        Map = map;
        TileSize = tileSize;
    }
}