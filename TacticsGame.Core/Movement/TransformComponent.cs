using System.Drawing;

namespace TacticsGame.Core.Movement;

public struct LocationComponent
{
    public (int, int) TileIndex { get; set; }
    public PointF Location { get; set; }

    public LocationComponent(/*(int , int) tileIndex, */PointF location)
    {
        //TileIndex = tileIndex; 
        Location = location;
    }
}