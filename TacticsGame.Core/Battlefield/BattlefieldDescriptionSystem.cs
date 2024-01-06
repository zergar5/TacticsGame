using Leopotam.EcsLite;
using TacticsGame.Core.Movement;

namespace TacticsGame.Core.Battlefield;

public class BattlefieldDescriptionSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsPool<LocationComponent> _locations;

    private BattlefieldTiles _battlefieldTiles;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _locations = world.GetPool<LocationComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        throw new NotImplementedException();
    }
}