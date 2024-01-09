using SharpGL.SceneGraph.Raytracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticsGame.Core.Weapons.Profiles;

namespace TacticsGame.Core.Units.Datasheets
{
    public class Termagant : UnitDatasheet
    {
        public Termagant()
        {
            Movement = 6;
            Toughness = 3;
            Save = 5;
            Wounds = 1;

            WeaponProfiles = new List<WeaponProfile>() { new Fleshborer() };
        }
    }
}
