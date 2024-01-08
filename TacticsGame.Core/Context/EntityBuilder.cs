using Leopotam.EcsLite;

namespace TacticsGame.Core.Context;

public class EntityBuilder
{
    private readonly EcsWorld _world;
    private int _entity = -1;

    public EntityBuilder(EcsWorld world)
    {
        _world = world;
    }

    public EntityBuilder Init()
    {
        _entity = _world.NewEntity();

        return this;
    }

    public EntityBuilder Set<T>(T component) where T : struct
    {
        if (_entity == -1) throw new Exception("You must create entity first.");

        var templateComponentPool = _world.GetPool<T>();
        templateComponentPool.Add(_entity) = component;

        return this;
    }

    public EntityBuilder Set<T>(int entity, T component) where T : struct
    {
        var templateComponentPool = _world.GetPool<T>();

        if (templateComponentPool.Has(entity))
            throw new Exception("Entity can have only one component of each type");

        templateComponentPool.Add(entity) = component;

        return this;
    }

    public int Build()
    {
        if (_entity == -1) throw new Exception("You must create entity first.");

        var entity = _entity;
        _entity = -1;

        return entity;
    }
}