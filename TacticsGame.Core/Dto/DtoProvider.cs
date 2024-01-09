using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using TacticsGame.Core.Damage;
using TacticsGame.Core.Providers;
using TacticsGame.Core.Render;
using TacticsGame.Core.Shooting;
using TacticsGame.Core.Units;

namespace TacticsGame.Core.Dto;

public class DtoProvider
{
    private AssetsProvider _assetsProvider;
    private EcsFilter _unitsFilter;
    private EcsFilter _weaponsFilter;

    private EcsPool<WoundsComponent> _wounds;
    private EcsPool<SpriteComponent> _sprites;
    private EcsPool<OwnershipComponent> _owners;

    public void SetWorld(EcsWorld world)
    {
        _unitsFilter = world.Filter<UnitProfileComponent>().End();
        _weaponsFilter = world.Filter<RangeWeaponProfileComponent>().End();

        _wounds = world.GetPool<WoundsComponent>();
        _sprites = world.GetPool<SpriteComponent>();
        _owners = world.GetPool<OwnershipComponent>();
    }

    public void SetAssetsProvider(AssetsProvider assetsProvider)
    {
        _assetsProvider = assetsProvider;
    }

    public UnitDto CreateUnitDto(int unitId)
    {
        var woundsComponent = _wounds.Get(unitId);
        //var spriteComponent = _sprites.Get(unitId);

        var weaponDtos = new Dictionary<int, WeaponDto>();

        foreach (var weapon in _weaponsFilter)
        {
            if (_owners.Get(weapon).OwnerId == unitId)
            {
                weaponDtos.Add(weapon, CreateWeaponDto(weapon));
            }
        }

        var unitDto = new UnitDto(unitId, woundsComponent.Wounds, woundsComponent.RemainingWounds,
                      _assetsProvider.GetPath(spriteComponent.Sprite), weaponDtos);

        return unitDto;
    }

    private WeaponDto CreateWeaponDto(int weaponId)
    {
        var spriteComponent = _sprites.Get(weaponId);

        var weaponDto = new WeaponDto(weaponId, _assetsProvider.GetPath(spriteComponent.Sprite));

        return weaponDto;
    }
}