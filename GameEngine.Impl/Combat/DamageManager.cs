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
        public void DoAttack(IAttackContainer attackContainer, IDefensesContainer defensesContainer)
        {
            Attack a = attackContainer.GetAttack();
            Defense d = defensesContainer.GetDefenses().GetDefense(a.DamageType);
            
            int ammount = a.DamageAmmount - d.DamageReduction;
            ammount -= (int) (ammount * d.DamageResistance);

            defensesContainer.Damage(ammount);
        }
    }
}
