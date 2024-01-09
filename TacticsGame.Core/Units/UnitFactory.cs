using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticsGame.Core.Context;
using TacticsGame.Core.Units.Datasheets;
using TacticsGame.Core.Weapons.Profiles;

namespace TacticsGame.Core.Units
{
    public class UnitFactory
    {
        private readonly Dictionary<int, Func<UnitDatasheet>> FactoryMethods = new();

        private readonly EntityBuilder _entityBuilder;

        public UnitFactory(EntityBuilder entityBuilder)
        {
            _entityBuilder = entityBuilder;
        }

        public void InitFactoryMethods()
        {
            FactoryMethods.Add(0, () => new NecronWarrior());
            FactoryMethods.Add(1, () => new Termagant());
        }

        public UnitDatasheet CreateUnit(int unitId)
        {
            var factoryMethod = FactoryMethods[unitId];
            var unit = factoryMethod.Invoke();
            return unit;
        }

        public int CreateUnitEntity(UnitDatasheet unitDatasheet)
        {
            var unitEntity = _entityBuilder
                .Init()
                .Set(new UnitProfileComponent())
                .Set(new UnitTurnStateComponent())
                .Build();

            return unitEntity;
        }
    }

    
}
