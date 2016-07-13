using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Map
{
    public class Attack
    {
        public Attack(int damage)
        {
            DamageAmmount = damage;
        }

        public int DamageAmmount { get; set; }
    }
}
