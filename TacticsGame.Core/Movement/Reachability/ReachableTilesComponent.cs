using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Movement.Reachability;

public struct ReachableTilesComponent
{
    public List<Tile> ReachableTiles { get; set; }

    public ReachableTilesComponent(List<Tile> reachableTiles)
    {
        ReachableTiles = reachableTiles;
    }
}