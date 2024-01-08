namespace TacticsGame.Core.Providers;

public class StateProvider
{
    private bool _isMoving;

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            if (_isShooting) return;
            _isMoving = value;
            OnMovingStateChanged(value);
        }
    }

    private bool _isShooting;

    public bool IsShooting
    {
        get => _isShooting;
        set
        {

            _isShooting = value;
            if (_isShooting) _isMoving = false;
            OnShootingStateChanged(value);
        }
    }

    public event EventHandler<bool> MovingStateChanged;

    public event EventHandler<bool> ShootingStateChanged;

    protected virtual void OnMovingStateChanged(bool value)
    {
        MovingStateChanged?.Invoke(this, value);
    }

    protected virtual void OnShootingStateChanged(bool value)
    {
        ShootingStateChanged?.Invoke(this, value);
    }
}