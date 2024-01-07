using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Context;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Movement.Reachability;

public class ReachableTilesFindingSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly EntityBuilder _entityBuilder;
    [EcsInject] private readonly Cartographer _cartographer;

    private EcsFilter _battlefieldFilter;
    private EcsFilter _currentUnitFilter;

    private EcsPool<BattlefieldComponent> _battlefields;
    private EcsPool<MovementComponent> _movements;
    private EcsPool<LocationComponent> _transforms;
    private EcsPool<ReachableTilesComponent> _reachableTiles;

    private BattlefieldTiles _battlefieldTiles;
    private BFS _bfs;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _battlefields = world.GetPool<BattlefieldComponent>();
        _movements = world.GetPool<MovementComponent>();
        _transforms = world.GetPool<LocationComponent>();
        _reachableTiles = world.GetPool<ReachableTilesComponent>();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().Inc<UnitProfileComponent>().End();
        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        foreach (var battlefield in _battlefieldFilter)
        {
            _battlefieldTiles = _battlefields.Get(battlefield).Map;
        }

        _bfs = new BFS(_battlefieldTiles);
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            if (!_movements.Get(currentUnit).IsMoving) continue;

            var position = _transforms.Get(currentUnit).Location;

            var (row, column) = _cartographer.FindIndex(position);

            var reachableTiles =
                _bfs.FindReachableTiles(row, column, _movements.Get(currentUnit).RemainingMovement);

            if (_reachableTiles.Has(currentUnit))
            {
                _reachableTiles.Get(currentUnit).ReachableTiles = reachableTiles;
            }
            else
            {
                _entityBuilder.Set(currentUnit, new ReachableTilesComponent(reachableTiles));
            }
        }
    }
}