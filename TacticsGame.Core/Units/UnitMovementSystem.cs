using Leopotam.EcsLite;
using TacticsGame.Core.Movement;

namespace TacticsGame.Core.Units;

public class UnitMovementSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsPool<LocationComponent> _transforms;
    private EcsPool<MovementComponent> _movements;

    private EcsFilter _currentUnitFilter;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _transforms = world.GetPool<LocationComponent>();
        _movements = world.GetPool<MovementComponent>();

        _currentUnitFilter = world.Filter<CurrentUnitMarker>().End();

        if (_currentUnitFilter.GetEntitiesCount() != 1) throw new Exception("Only one unit can be current");

        //Наверное нужен фильтр по текущему юниту
    }

    public void Run(IEcsSystems systems)
    {
        //Нужно вытащить текущую позицию, если она не та же самая, то проверить на сколько может пройти юнит
        //Построить путь через AStar, если длина пути превышает максимальную/оставшуюся дистанция, подвинуть на возможную



    }
}