using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Combat
{
    public class Attack
    {
        public Attack(int damage, EDamageType damageType)
        {
            DamageAmmount = damage;
            DamageType = damageType;
        }

        public int DamageAmmount { get; set; }
        public EDamageType DamageType { get; set; }
    }
}
