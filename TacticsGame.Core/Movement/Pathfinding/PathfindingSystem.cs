using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Context;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Handlers.UnitStateHandlers;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Scene;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Movement.Pathfinding;

public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private readonly MouseTargetPositionHandler _positionHandler;
    [EcsInject] private readonly EntityBuilder _entityBuilder;
    [EcsInject] private readonly Cartographer _cartographer;

    private EcsFilter _battlefieldFilter;
    private EcsFilter _currentUnitFilter;

    private EcsPool<BattlefieldComponent> _battlefields;
    private EcsPool<MovementComponent> _movements;
    private EcsPool<LocationComponent> _transforms;
    private EcsPool<ReachableTilesComponent> _reachableTiles;
    private EcsPool<PathComponent> _paths;

    private BattlefieldTiles _battlefieldTiles;
    private AStar _aStar;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _battlefields = world.GetPool<BattlefieldComponent>();
        _movements = world.GetPool<MovementComponent>();
        _transforms = world.GetPool<LocationComponent>();
        _reachableTiles = world.GetPool<ReachableTilesComponent>();
        _paths = world.GetPool<PathComponent>();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().Inc<UnitProfileComponent>().End();
        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        foreach (var battlefield in _battlefieldFilter)
        {
            ref var battlefieldComponent = ref _battlefields.Get(battlefield);

            _battlefieldTiles = battlefieldComponent.Map;
        }

        _aStar = new AStar(_battlefieldTiles);
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            if (!_movements.Get(currentUnit).IsMoving) continue;

            var targetPosition = _positionHandler.GetPosition();

            var position = _transforms.Get(currentUnit).Location;

            var (row, column) = _cartographer.FindIndex(position);

            var (targetRow, targetColumn) = _cartographer.FindIndex(targetPosition);

            if (targetRow == -1 && targetColumn == -1) continue;

            var targetTile = _battlefieldTiles[targetRow, targetColumn];

            if(!_reachableTiles.Get(currentUnit).ReachableTiles.Contains(targetTile)) continue;

            var path = _aStar.FindPath(row, column, targetRow, targetColumn);

            if (_paths.Has(currentUnit))
            {
                _paths.Get(currentUnit).Path = path;
            }
            else
            {
                _entityBuilder.Set(currentUnit, new PathComponent(path));
            }
        }
    }
}