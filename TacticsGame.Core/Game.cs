using System.Collections.ObjectModel;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Context;
using TacticsGame.Core.Converters;
using TacticsGame.Core.Damage;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Handlers.StateHandlers;
using TacticsGame.Core.Mechanics;
using TacticsGame.Core.Mechanics.Queue;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Movement.Pathfinding;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Providers;
using TacticsGame.Core.Render;
using TacticsGame.Core.Scene;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Units;

namespace TacticsGame.Core;

public class Game
{
    private readonly EcsWorld _world;
    private readonly EntityBuilder _entityBuilder;

    private readonly IBattlefieldGenerator _battlefieldGenerator;
    private readonly Cartographer _cartographer;
    private readonly MousePositionProvider _positionProvider;
    private readonly CoordinatesConverter _coordinatesConverter;

    private readonly EcsSystems _setupSystems;
    private readonly EcsSystems _gameplaySystems;
    private readonly EcsSystems _movementSystems;
    private readonly EcsSystems _shootingSystems;
    private readonly EcsSystems _transformSystems;
    private EcsSystems _renderSystems;

    public Game
    (
        IBattlefieldGenerator battlefieldGenerator,
        MousePositionProvider positionProvider,
        StateProvider stateProvider,
        CoordinatesConverter coordinatesConverter,
        ObservableCollection<int> units
    )
    {
        _world = new EcsWorld();
        _entityBuilder = new EntityBuilder(_world);

        _battlefieldGenerator = battlefieldGenerator;
        _positionProvider = positionProvider;
        _coordinatesConverter = coordinatesConverter;

        _setupSystems = new EcsSystems(_world);
        _setupSystems
            .Add(new InitBattlefieldSystem())
            .Add(new UnitSpawnerSystem())
            .Inject(_entityBuilder, _battlefieldGenerator)
            .Init();

        _cartographer = new Cartographer(_world);

        _gameplaySystems = new EcsSystems(_world);
        _gameplaySystems
            .Add(new GameQueueSystem())
            .Inject(new GameQueue(units), _entityBuilder, new MovingStateHandler(stateProvider))
            .Init();

        var mouseTargetPositionHandler = new MouseTargetPositionHandler(_positionProvider, coordinatesConverter);

        _movementSystems = new EcsSystems(_world);
        _movementSystems
            .Add(new ReachableTilesFindingSystem())
            .Add(new PathfindingSystem())
            .Inject(mouseTargetPositionHandler, _entityBuilder)
            .Inject(_cartographer)
            .Init();

        _shootingSystems = new EcsSystems(_world);
        _shootingSystems
            .Add(new RangeTargetingSystem())
            .Add(new ShootingSystem())
            .Add(new DamageSystem())
            .Inject(mouseTargetPositionHandler, new ShootingStateHandler(stateProvider))
            .Inject(_cartographer, _entityBuilder, new DiceRoller())
            .Init();

        _transformSystems = new EcsSystems(_world);
        _transformSystems
            .Add(new TransformSystem())
            .Init();
    }

    public void InitRenderSystems(OpenGL gl)
    {
        _renderSystems = new EcsSystems(_world);
        _renderSystems
            .Add(new InitRenderSystem())
            .Add(new BattlefieldRenderSystem())
            .Add(new UIRenderSystem())
            .Add(new UnitsRenderSystem())
            .Inject(gl, _cartographer, new MousePositionHandler(_positionProvider, _coordinatesConverter))
            .Init();
    }

    public void Update()
    {
        _gameplaySystems.Run();
        _movementSystems.Run();
        _shootingSystems.Run();
        _transformSystems.Run();
    }

    public void Render()
    {
        _renderSystems.Run();
    }
}