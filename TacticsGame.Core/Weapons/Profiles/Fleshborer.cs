using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsGame.Core.Weapons.Profiles
{
    public class Fleshborer : WeaponProfile
    {
        public Fleshborer() 
        {
            Range = 18;
            NumberOfShots = 1;
            BallisticSkill = 4;
            Strength = 5;
            AP = 0;
            Damage = 1;
        }
    }
}
