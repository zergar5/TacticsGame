using System.Collections.ObjectModel;

namespace TacticsGame.Core.Mechanics.Queue;

public class GameQueue
{
    private readonly ObservableCollection<int> _unitsQueue;
    private int _currentIndex = -1;

    public GameQueue(ObservableCollection<int> unitsQueue)
    {
        _unitsQueue = unitsQueue;
    }

    public void SetUnits(Dictionary<int, int> unitsMovements)
    {
        var orderedUnits = OrderUnitsByMovement(unitsMovements);

        foreach (var unit in orderedUnits)
        {
            _unitsQueue.Add(unit);
        }
    }

    public int CurrentUnit()
    {
        return _unitsQueue[_currentIndex];
    }

    public int NextUnit()
    {
        if (_currentIndex == _unitsQueue.Count - 1 || _currentIndex == -1)
        {
            _currentIndex = 0;
        }
        else
        {
            _currentIndex++;
        }

        var currentUnit = _unitsQueue[_currentIndex];

        return currentUnit;
    }

    public void RemoveUnit(int unit)
    {
        _unitsQueue.Remove(unit);
    }

    public void UpdateOrder(Dictionary<int, int> unitsMovements)
    {
        var orderedUnits = OrderUnitsByMovement(unitsMovements);

        for (var i = 0; i < _unitsQueue.Count; i++)
        {
            if (orderedUnits[i] == _unitsQueue[i]) continue;

            var newIndex = orderedUnits.IndexOf(_unitsQueue[i]);

            _unitsQueue.Move(i, newIndex);
        }
    }

    public List<int> OrderUnitsByMovement(Dictionary<int, int> unitsMovements)
    {
        var orderedUnits = unitsMovements
            .OrderBy(x => x.Value)
            .Select(x => x.Key)
            .ToList();

        return orderedUnits;
    }
}