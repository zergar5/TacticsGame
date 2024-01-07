﻿using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Context;
using TacticsGame.Core.Movement;

namespace TacticsGame.Core.Units;

public class UnitSpawnerSystem : IEcsInitSystem
{
    [EcsInject] private EntityBuilder _entityBuilder;

    public void Init(IEcsSystems systems)
    {
        var units = new List<int> { 1 };

        CreateUnits(units);
    }

    private void CreateUnits(List<int> units)
    {
        foreach (var unit in units)
        {
            _entityBuilder
                .Init()
                .Set(new UnitProfileComponent(5, 4, 4, 1))
                .Set(new MovementComponent(5))
                .Set(new LocationComponent(new PointF(-0.25f, 0.25f)))
                .Set(new CurrentUnitMarker())
                .Build();
        }
    }
}