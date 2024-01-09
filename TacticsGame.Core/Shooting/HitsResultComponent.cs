namespace TacticsGame.Core.Shooting;

public struct ShootingResultComponent
{
    public int SuccessfulHits { get; set; }
    public int SuccessfulWounds { get; set; }
    public int Сasualties { get; set; }

    public ShootingResultComponent(int successfulHits)
    {
        SuccessfulHits = successfulHits;
    }
}