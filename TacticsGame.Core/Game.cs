using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using SharpGL;
using TacticsGame.Core.Battlefield;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Context;
using TacticsGame.Core.Converters;
using TacticsGame.Core.Handlers;
using TacticsGame.Core.Handlers.MousePositionHandlers;
using TacticsGame.Core.Mechanics.Queue;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Movement.Pathfinding;
using TacticsGame.Core.Movement.Reachability;
using TacticsGame.Core.Providers;
using TacticsGame.Core.Render;
using TacticsGame.Core.Units;

namespace TacticsGame.Core;

public class Game
{
    private readonly EcsWorld _world;

    private readonly EntityBuilder _entityBuilder;

    private readonly EcsSystems _setupSystems;
    private readonly EcsSystems _gameplaySystems;
    private readonly EcsSystems _movementSystems;
    private readonly EcsSystems _transformSystems;
    private EcsSystems _renderSystems;

    public Game
    (
        IBattlefieldGenerator battlefieldGenerator, 
        MousePositionProvider positionProvider, 
        CoordinatesConverter coordinatesConverter
    )
    {
        _world = new EcsWorld();
        _entityBuilder = new EntityBuilder(_world);

        _setupSystems = new EcsSystems(_world);
        _setupSystems
            .Add(new InitBattlefieldSystem())
            .Add(new UnitSpawnerSystem())
            .Inject(_entityBuilder, battlefieldGenerator)
            .Init();

        _gameplaySystems = new EcsSystems(_world);
        _gameplaySystems
            .Add(new GameQueueSystem())
            .Inject(_entityBuilder, battlefieldGenerator)
            .Init();

        _movementSystems = new EcsSystems(_world);
        _movementSystems
            .Add(new ReachableTilesFindingSystem())
            .Add(new PathfindingSystem())
            .Inject(new MouseTargetPositionHandler(positionProvider, coordinatesConverter), _entityBuilder)
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
            .Add(new BattlefieldRenderSystem())
            .Add(new UnitsRenderSystem())
            .Inject(gl)
            .Init();
    }

    public void Update()
    {
        _transformSystems.Run();
        _movementSystems.Run();
    }

    public void Render()
    {
        _renderSystems.Run();
    }
}