using System.Drawing;

namespace TacticsGame.Core.Movement;

public struct TransformComponent
{
    public PointF Location { get; set; }

    public TransformComponent(PointF location)
    {
        Location = location;
    }
}