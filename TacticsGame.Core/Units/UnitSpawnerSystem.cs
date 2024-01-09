using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Context;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Weapons;

namespace TacticsGame.Core.Units;

public class UnitSpawnerSystem : IEcsInitSystem
{
    [EcsInject] private EntityBuilder _entityBuilder;
    [EcsInject] private WeaponFactory _weaponFactory;
    [EcsInject] private UnitFactory _unitFactory;

    private List<PointF> _positions = new() { new(0.25f, 0.25f), new(-0.25f, -0.25f) };

    public void Init(IEcsSystems systems)
    {
        var units = new List<int> { 1, 1 };

        CreateUnits(units);
    }

    private void CreateUnits(List<int> units)
    {
        for (var i = 0; i < units.Count; i++)
        {
            _entityBuilder
                .Init()
                .Set(new UnitProfileComponent(5, 4, 4, 1))
                .Set(new MovementComponent(5))
                .Set(new LocationComponent(_positions[i]))
                .Set(new UnitTurnStateComponent())
                .Build();
        }
    }
}