using System.Drawing;

namespace TacticsGame.Core.Providers;

public class MousePositionProvider
{
    private PointF _position;

    public PointF Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPositionChanged(value);
        }
    }

    private PointF _targetPosition;

    public PointF TargetPosition
    {
        get => _targetPosition;
        set
        {
            _targetPosition = value;
            OnTargetPositionChanged(value);
        }
    }

    public event EventHandler<PointF> PositionChanged;

    public event EventHandler<PointF> TargetPositionChanged;

    protected virtual void OnPositionChanged(PointF value)
    {
        PositionChanged?.Invoke(this, value);
    }

    protected virtual void OnTargetPositionChanged(PointF value)
    {
        TargetPositionChanged?.Invoke(this, value);
    }
}