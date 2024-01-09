using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticsGame.Core.Weapons.Profiles;

namespace TacticsGame.Core.Units.Datasheets
{
    public class UnitDatasheet
    {
        public int Movement { get; set; }
        public int Toughness { get; set; }
        public int Save { get; set; }
        public int Wounds { get; set; }

        public List<WeaponProfile> WeaponProfiles { get; set; }

        public UnitDatasheet() 
        {
          
        }
    }
}
