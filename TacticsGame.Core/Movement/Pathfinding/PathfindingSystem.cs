using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.ComponentModel;
using System.Drawing;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Context;
using TacticsGame.Core.Handlers;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Providers;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Movement.Pathfinding;

public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
{
    [EcsInject] private MouseTargetPositionHandler _positionHandler;
    [EcsInject] private readonly EntityBuilder _entityBuilder;
    [EcsInject] private readonly Cartographer _cartographer;

    private EcsFilter _battlefieldFilter;
    private EcsFilter _currentUnitFilter;

    private EcsPool<BattlefieldComponent> _battlefields;
    private EcsPool<MovementComponent> _movements;
    private EcsPool<LocationComponent> _transforms;
    private EcsPool<PathComponent> _paths;

    private BattlefieldTiles _battlefieldTiles;
    private AStar _aStar;
    private PointF _position;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _battlefields = world.GetPool<BattlefieldComponent>();
        _movements = world.GetPool<MovementComponent>();
        _transforms = world.GetPool<LocationComponent>();
        _paths = world.GetPool<PathComponent>();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().Inc<UnitProfileComponent>().End();
        _battlefieldFilter = world.Filter<BattlefieldComponent>().End();

        foreach (var battlefield in _battlefieldFilter)
        {
            ref var battlefieldComponent = ref _battlefields.Get(battlefield);

            _battlefieldTiles = battlefieldComponent.Map;
        }

        foreach (var currentUnit in _currentUnitFilter)
        {
            _position = _transforms.Get(currentUnit).Location;
        }

        _aStar = new AStar(_battlefieldTiles);
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var currentUnit in _currentUnitFilter)
        {
            if (!_movements.Get(currentUnit).IsMoving) continue;

            _position = _positionHandler.GetPosition();

            var position = _transforms.Get(currentUnit).Location;

            var (row, column) = _cartographer.FindIndex(position);

            var (targetRow, targetColumn) = _cartographer.FindIndex(_position);

            if (targetRow == -1 || targetColumn == -1) continue;

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