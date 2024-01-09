using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using TacticsGame.Core.Context;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Weapons.Profiles;

namespace TacticsGame.Core.Weapons;

public class WeaponFactory
{
    private readonly Dictionary<int, Func<WeaponProfile>> FactoryMethods = new();

    private readonly EntityBuilder _entityBuilder;

    public WeaponFactory(EntityBuilder entityBuilder)
    {
        _entityBuilder = entityBuilder;
    }

    public void InitFactoryMethods()
    {
        FactoryMethods.Add(0, () => new GaussFlayer());
        FactoryMethods.Add(1, () => new Fleshborer());
    }

    public WeaponProfile CreateWeapon(int weaponId)
    {
        var factoryMethod = FactoryMethods[weaponId];
        var weapon = factoryMethod.Invoke();
        return weapon;
    }

    public int CreateWeaponEntity(WeaponProfile weaponProfile)
    {
        var weaponEntity = _entityBuilder
            .Init()
            .Set(new RangeWeaponProfileComponent(weaponProfile))
            .Set(new RangeWeaponComponent())
            .Build();

        return weaponEntity;
    }
}