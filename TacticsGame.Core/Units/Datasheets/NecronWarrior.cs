using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticsGame.Core.Dto;
using TacticsGame.Core.Weapons.Profiles;

namespace TacticsGame.Core.Units.Datasheets
{
    public class NecronWarrior : UnitDatasheet
    {
        public NecronWarrior()
        {
            Movement = 5;
            Toughness = 4;
            Save = 4;
            Wounds = 1;

            WeaponProfiles = new List<WeaponProfile>() { new GaussFlayer() };
        }
    }
}
