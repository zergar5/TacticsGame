namespace TacticsGame.Core.Mechanics.Queue;

public class GameQueue
{
    private readonly LinkedList<int> _unitsQueue;

    public GameQueue(List<int> units)
    {
        _unitsQueue = new LinkedList<int>(units);
    }

    public int NextUnit()
    {
        var currentUnit = _unitsQueue.First;

        var nextUnit = currentUnit.Next;

        return nextUnit.Value;
    }

    public int NextTurn()
    {
        return 0;
    }
}