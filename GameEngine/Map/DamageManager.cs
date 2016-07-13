using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Map
{
    public class DamageManager : IDamageManager
    {
        public void DoDamage(IAttackContainer attackContainer, IDefensesContainer defensesContainer)
        {
            Attack a = attackContainer.GetAttack();
            Defenses d = defensesContainer.GetDefenses();

            int ammount = a.DamageAmmount - d.DamageReduction;
            ammount -= (int) (ammount * d.DamageResistence);

            defensesContainer.Damage(ammount);
        }
    }
}
