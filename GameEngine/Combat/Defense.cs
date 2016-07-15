using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Combat
{
    public struct Defense
    {
        public int DamageReduction { get; set; }
        public float DamageResistance { get; set; }

        public static Defense Zero = new Defense();

        public static Defense FromResistance(float resistance)
        {
            return new Defense() { DamageResistance = resistance };
        }

        public static Defense FromReduction(int reduction)
        {
            return new Defense() { DamageReduction = reduction };
        }
    }
}
