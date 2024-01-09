using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsGame.Core.Weapons.Profiles
{
    public class GaussFlayer : WeaponProfile
    {
        public GaussFlayer() 
        {
            Range = 24;
            NumberOfShots = 1;
            BallisticSkill = 4;
            Strength = 4;
            AP = 0;
            Damage = 1;
        }
    }
}
