using Leopotam.EcsLite;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Mechanics.Queue;

public class GameQueueSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _unitsFilter;
    private EcsPool<UnitProfileComponent> _units;

    private GameQueue _queue;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _unitsFilter = world.Filter<UnitProfileComponent>().End();
        _units = world.GetPool<UnitProfileComponent>();

        var list = new List<int>();

        foreach (var unit in _unitsFilter)
        {
            list.Add(unit);
        }

        _queue = new GameQueue(list);
    }

    public void Run(IEcsSystems systems)
    {

    }
}