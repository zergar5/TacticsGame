﻿using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Scene;

namespace TacticsGame.Core.Movement.Pathfinding;

public class AStar
{
    private readonly Cartographer _cartographer;
    private readonly BattlefieldTiles _tiles;
    private readonly List<Tile> _path;
    private readonly PriorityQueue<(int, int), int> _openSet;
    private readonly HashSet<(int, int)> _closedSet;
    private readonly Dictionary<(int, int), (int, int)> _cameFrom;
    private readonly Dictionary<(int, int), int> _pathCost;
    private readonly List<(int, int)> _neighbors;

    public AStar(Cartographer cartographer, BattlefieldTiles tiles)
    {
        _cartographer = cartographer;
        _tiles = tiles;
        _path = new List<Tile>();
        _openSet = new PriorityQueue<(int, int), int>();
        _closedSet = new HashSet<(int, int)>();
        _cameFrom = new Dictionary<(int, int), (int, int)>();
        _pathCost = new Dictionary<(int, int), int>();
        _neighbors = new List<(int, int)>();
    }

    public List<Tile> FindPath(int startRow, int startColumn, int targetRow, int targetColumn)
    {
        _openSet.Clear();
        _closedSet.Clear();
        _cameFrom.Clear();
        _pathCost.Clear();

        _pathCost[(startRow, startColumn)] = 0;

        _openSet.Enqueue((startRow, startColumn), HeuristicCostEstimate(startRow, startColumn, targetRow, targetColumn));

        while (_openSet.Count > 0)
        {
            var currentTile = _openSet.Dequeue();

            var row = currentTile.Item1;
            var column = currentTile.Item2;

            if (currentTile == (targetRow, targetColumn)) break;

            _closedSet.Add(currentTile);

            foreach (var neighbor in GetNeighbors(row, column))
            {
                if (_closedSet.Contains(neighbor)) continue;

                var currentCost = _pathCost[currentTile] + 1;

                if (!_pathCost.ContainsKey(neighbor) || currentCost < _pathCost[neighbor])
                {
                    _cameFrom[neighbor] = currentTile;
                    _pathCost[neighbor] = currentCost;
                    var heuristicCost = currentCost +
                                        HeuristicCostEstimate(neighbor.row, neighbor.column,
                                            targetRow, targetColumn);

                    if (_openSet.UnorderedItems.All(n => n.Element != neighbor))
                    {
                        _openSet.Enqueue(neighbor, heuristicCost);
                    }
                }
            }
        }

        ReconstructPath(targetRow, targetColumn);

        return _path;
    }

    private List<(int row, int column)> GetNeighbors(int row, int column)
    {
        _neighbors.Clear();

        if (column > 0 && _tiles[row, column - 1].Type == TileType.Field &&
            !_cartographer.CheckTileForUnit(_tiles[row, column - 1]))
        {
            _neighbors.Add((row, column - 1));
        }

        if (row > 0 && _tiles[row - 1, column].Type == TileType.Field &&
            !_cartographer.CheckTileForUnit(_tiles[row - 1, column]))
        {
            _neighbors.Add((row - 1, column));
        }

        if (column < _tiles.CountColumns - 1 && _tiles[row, column + 1].Type == TileType.Field &&
            !_cartographer.CheckTileForUnit(_tiles[row, column + 1]))
        {
            _neighbors.Add((row, column + 1));
        }

        if (row < _tiles.CountRows - 1 && _tiles[row + 1, column].Type == TileType.Field &&
            !_cartographer.CheckTileForUnit(_tiles[row + 1, column]))
        {
            _neighbors.Add((row + 1, column));
        }

        return _neighbors;
    }

    private int HeuristicCostEstimate(int startRow, int startColumn, int targetRow, int targetColumn)
    {
        return Math.Abs(startRow - targetRow) + Math.Abs(startColumn - targetColumn);
    }

    private void ReconstructPath(int targetRow, int targetColumn)
    {
        _path.Clear();

        _path.Add(_tiles[targetRow, targetColumn]);

        while (_cameFrom.ContainsKey((targetRow, targetColumn)))
        {
            (targetRow, targetColumn) = _cameFrom[(targetRow, targetColumn)];
            _path.Insert(0, _tiles[targetRow, targetColumn]);
        }
    }
}