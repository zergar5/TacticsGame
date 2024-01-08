using System.Drawing;

namespace TacticsGame.Core.Handlers.MousePositionHandlers;

public interface IMousePositionHandler
{
    public PointF GetPosition();
}