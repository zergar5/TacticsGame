using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Movement.Reachability;

public class BFS
{
    private readonly BattlefieldTiles _tiles;
    private readonly List<Tile> _reachableTiles;
    private readonly Queue<(int, int, int)> _queue;
    private readonly HashSet<(int, int)> _visitedTiles;
    private readonly List<(int, int)> _neighbors;

    public BFS(BattlefieldTiles tiles)
    {
        _tiles = tiles;
        _reachableTiles = new List<Tile>();
        _queue = new Queue<(int, int, int)>();
        _visitedTiles = new HashSet<(int, int)>();
        _neighbors = new List<(int, int)>();
    }

    public List<Tile> FindReachableTiles(int startRow, int startColumn, int movement)
    {
        _reachableTiles.Clear();
        _queue.Clear();
        _visitedTiles.Clear();

        _queue.Enqueue((startRow, startColumn, 0));
        _visitedTiles.Add((startRow, startColumn));

        while (_queue.Count > 0)
        {
            var (currentRow, currentColumn, traveledDistance) = _queue.Dequeue();

            if (traveledDistance > movement) continue;

            _reachableTiles.Add(_tiles[currentRow, currentColumn]);

            foreach (var neighbor in GetNeighbors(currentRow, currentColumn))
            {
                if (_visitedTiles.Contains(neighbor)) continue;

                _queue.Enqueue((neighbor.Item1, neighbor.Item2, traveledDistance + 1));

                _visitedTiles.Add(neighbor);
            }
        }

        return _reachableTiles;
    }

    private List<(int, int)> GetNeighbors(int row, int column)
    {
        _neighbors.Clear();

        if (column > 0 && _tiles[row, column - 1].Type == TileType.Field)
        {
            _neighbors.Add((row, column - 1));
        }

        if (row > 0 && _tiles[row - 1, column].Type == TileType.Field)
        {
            _neighbors.Add((row - 1, column));
        }

        if (column < _tiles.CountColumns - 1 && _tiles[row, column + 1].Type == TileType.Field)
        {
            _neighbors.Add((row, column + 1));
        }

        if (row < _tiles.CountRows - 1 && _tiles[row + 1, column].Type == TileType.Field)
        {
            _neighbors.Add((row + 1, column));
        }

        return _neighbors;
    }
}