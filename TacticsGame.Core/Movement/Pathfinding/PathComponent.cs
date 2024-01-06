using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Movement.Pathfinding;

public struct PathComponent
{
    public List<Tile> Path { get; set; }

    public PathComponent(List<Tile> path)
    {
        Path = path;
    }
}