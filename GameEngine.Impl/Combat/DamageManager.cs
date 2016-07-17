using GameEngine.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Combat
{
    public class DamageManager : IDamageManager
    {
        public void DoAttack(Attack attack, IDefensesContainer defensesContainer)
        {
            Defense d = defensesContainer.GetDefenses().GetDefense(attack.DamageType);
            
            int ammount = attack.DamageAmmount - d.DamageReduction;
            ammount -= (int) (ammount * d.DamageResistance);

            defensesContainer.Damage(ammount);
            if(attack.HasStatus())
            {
                defensesContainer.AddStatus(attack.Status);
            }
        }
    }
}
