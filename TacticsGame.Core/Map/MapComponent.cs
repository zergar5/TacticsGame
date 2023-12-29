using System.Drawing;

namespace TacticsGame.Core.Map;

public class MapComponent
{
    public Map Map { get; }
    public Size TileSize { get; }

    public MapComponent(Map map)
    {
        Map = map;
        TileSize = new Size();
    }
}