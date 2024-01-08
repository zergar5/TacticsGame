using System.Drawing;

namespace TacticsGame.Core.Movement;

public struct LocationComponent
{
    public PointF Location { get; set; }

    public LocationComponent(PointF location)
    {
        Location = location;
    }
}