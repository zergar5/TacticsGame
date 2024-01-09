using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsGame.Core.Weapons.Profiles
{
    public class WeaponProfile
    {
        public int Range { get; set; }
        public int NumberOfShots { get; set; }
        public int BallisticSkill { get; set; }
        public int Strength { get; set; }
        public int AP { get; set; }
        public int Damage { get; set; }
    }
}
