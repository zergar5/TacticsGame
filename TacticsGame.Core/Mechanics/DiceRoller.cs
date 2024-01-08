namespace TacticsGame.Core.Mechanics;

public class DiceRoller
{
    private readonly Random _random = new();
    private readonly List<int> _rollResults = new();

    public int RollD6()
    {
        var result = _random.Next(1, 7);

        return result;
    }

    public List<int> RollD6(int dicesNumber)
    {
        _rollResults.Clear();

        for (var i = 0; i < dicesNumber; i++)
        {
            _rollResults.Add(RollD6());
        }

        return _rollResults;
    }
}