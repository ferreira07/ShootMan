using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Combat
{
    public class Defenses: Dictionary<EDamageType, Defense>
    {
        public void AddImunity(EDamageType damageType)
        {
            this[damageType] = Defense.FromResistance(1f);
        }
        
        public void AddResistance(EDamageType damageType, float damageResistance)
        {
            this[damageType] = Defense.FromResistance(damageResistance);
        }

        public Defense GetDefense(EDamageType damageType)
        {
            Defense ret;
            if (!this.TryGetValue(damageType, out ret))
            {
                ret = Defense.Zero;
            }
            return ret;
        }
    }    
}
