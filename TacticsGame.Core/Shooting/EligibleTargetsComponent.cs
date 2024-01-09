using TacticsGame.Core.Battlefield;

namespace TacticsGame.Core.Shooting;

public struct EligibleTargetsComponent
{
    public List<Tile> EligibleTargetsTiles { get; set; }

    public EligibleTargetsComponent(List<Tile> eligibleTargetsTiles)
    {
        EligibleTargetsTiles = eligibleTargetsTiles;
    }
}