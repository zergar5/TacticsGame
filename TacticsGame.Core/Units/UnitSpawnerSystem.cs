using Leopotam.EcsLite;
using SevenBoldPencil.EasyDi;
using System.Drawing;
using TacticsGame.Core.Context;
using TacticsGame.Core.Movement;
using TacticsGame.Core.Providers;
using TacticsGame.Core.Render;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Weapons;

namespace TacticsGame.Core.Units;

public class UnitSpawnerSystem : IEcsInitSystem
{
    [EcsInject] private EntityBuilder _entityBuilder;
    [EcsInject] private WeaponFactory _weaponFactory;
    [EcsInject] private UnitFactory _unitFactory;
    [EcsInject] private AssetsProvider _assetsProvider;

    public void Init(IEcsSystems systems)
    {
        CreateUnits();
    }

    private void CreateUnits()
    {
        var unit1 = _unitFactory.CreateUnitEntity(_unitFactory.CreateUnit(0));
        var unit2 = _unitFactory.CreateUnitEntity(_unitFactory.CreateUnit(0));
        //var unit3 = _unitFactory.CreateUnitEntity(_unitFactory.CreateUnit(0));
        //var unit4 = _unitFactory.CreateUnitEntity(_unitFactory.CreateUnit(1));
        //var unit5 = _unitFactory.CreateUnitEntity(_unitFactory.CreateUnit(1));
        //var unit6 = _unitFactory.CreateUnitEntity(_unitFactory.CreateUnit(1));

        _entityBuilder
            .Set(unit1, new OwnershipComponent(0))
            .Set(unit2, new OwnershipComponent(0));
            //.Set(unit3, new OwnershipComponent(0))
            //.Set(unit4, new OwnershipComponent(1))
            //.Set(unit5, new OwnershipComponent(1))
            //.Set(unit6, new OwnershipComponent(1));

            _entityBuilder
                .Set(unit1, new LocationComponent(new PointF(-0.25f, 2.25f)))
                .Set(unit2, new LocationComponent(new PointF(-1.75f, 2.25f)));
        //.Set(unit3, new LocationComponent(new PointF(0.75f, 2.25f)))
        //.Set(unit4, new LocationComponent(new PointF(0.25f, -3f)))
        //.Set(unit5, new LocationComponent(new PointF(-0.75f, -3f)))
        //.Set(unit6, new LocationComponent(new PointF(-1.75f, -3f)));

        _entityBuilder
            .Set(unit1, new SpriteComponent("NecronWarrior"))
            .Set(unit2, new SpriteComponent("NecronWarrior"));
        //.Set(unit3, new LocationComponent(new PointF(0f, 2.25f)))
        //.Set(unit4, new LocationComponent(new PointF(0f, -3f)))
        //.Set(unit5, new LocationComponent(new PointF(0f, -3f)))
        //.Set(unit6, new LocationComponent(new PointF(0f, -3f)));

        var weapon1 = _weaponFactory.CreateWeaponEntity(_weaponFactory.CreateWeapon(0));
        var weapon2 = _weaponFactory.CreateWeaponEntity(_weaponFactory.CreateWeapon(0));
        //var weapon3 = _weaponFactory.CreateWeaponEntity(_weaponFactory.CreateWeapon(0));
        //var weapon4 = _weaponFactory.CreateWeaponEntity(_weaponFactory.CreateWeapon(1));
        //var weapon5 = _weaponFactory.CreateWeaponEntity(_weaponFactory.CreateWeapon(1));
        //var weapon6 = _weaponFactory.CreateWeaponEntity(_weaponFactory.CreateWeapon(1));

        _entityBuilder
            .Set(weapon1, new OwnershipComponent(unit1))
            .Set(weapon2, new OwnershipComponent(unit2));
        //.Set(weapon3, new OwnershipComponent(unit3))
        //.Set(weapon4, new OwnershipComponent(unit4))
        //.Set(weapon5, new OwnershipComponent(unit5))
        //.Set(weapon6, new OwnershipComponent(unit6));

        _entityBuilder
            .Set(weapon1, new SpriteComponent("GaussFlayer"))
            .Set(weapon2, new SpriteComponent("GaussFlayer"));
        //.Set(weapon3, new OwnershipComponent(unit3))
        //.Set(weapon4, new OwnershipComponent(unit4))
        //.Set(weapon5, new OwnershipComponent(unit5))
        //.Set(weapon6, new OwnershipComponent(unit6));
    }
}