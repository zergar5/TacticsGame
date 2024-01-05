using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Algorithms;

public class BFS
{
    private readonly Tile[,] _tiles;
    private readonly List<Tile> _reachableTiles;
    private readonly Queue<(int, int, int)> _queue;
    private readonly HashSet<(int, int)> _visitedTiles;
    private readonly List<(int, int)> _neighbors;

    public BFS(Tile[,] tiles)
    {
        _tiles = tiles;
        _reachableTiles = new List<Tile>();
        _queue = new Queue<(int, int, int)>();
        _visitedTiles = new HashSet<(int, int)>();
        _neighbors = new List<(int, int)>();
    }

    public List<Tile> FindReachableTiles(int row, int column, int range)
    {
        //var tuple = new Tuple<int, int>(row, column);

        _reachableTiles.Clear();
        _queue.Clear();
        _visitedTiles.Clear();

        _queue.Enqueue((row, column, 0));
        _visitedTiles.Add((row, column));

        while (_queue.Count > 0)
        {
            var currentTile = _queue.Dequeue();
            row = currentTile.Item1;
            column = currentTile.Item2;
            var traveledRange = currentTile.Item3;

            if (traveledRange > range) continue;

            _reachableTiles.Add(_tiles[row, column]);

            foreach (var neighbor in GetNeighbors(row, column))
            {
                if (_visitedTiles.Contains(neighbor)) continue;

                _queue.Enqueue((neighbor.Item1, neighbor.Item2, traveledRange + 1));

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

        if (column < _tiles.GetLength(1) - 1 && _tiles[row, column + 1].Type == TileType.Field)
        {
            _neighbors.Add((row, column + 1));
        }

        if (row < _tiles.GetLength(0) - 1 && _tiles[row + 1, column].Type == TileType.Field)
        {
            _neighbors.Add((row + 1, column));
        }

        return _neighbors;
    }
}