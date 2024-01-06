using System.Drawing;
using TacticsGame.Core.Converters;
using TacticsGame.Core.Providers;

namespace TacticsGame.Core.Handlers.MousePositionHandlers;

public class MousePositionHandler : IMousePositionHandler
{
    private readonly CoordinatesConverter _converter;
    private PointF _mousePosition;

    public MousePositionHandler(MousePositionProvider positionProvider, CoordinatesConverter converter)
    {
        positionProvider.PositionChanged += HandlePositionChanged;
        _converter = converter;
    }

    public PointF GetPosition()
    {
        return _mousePosition;
    }

    private void HandlePositionChanged(object sender, PointF position)
    {
        _mousePosition = _converter.ConvertScreenToWorld(position);
    }
}