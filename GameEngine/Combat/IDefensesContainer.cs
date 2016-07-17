using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Combat
{
    public interface IDefensesContainer
    {
        int Hp { get; }
        Defenses GetDefenses();
        void Damage(int ammount);
        void AddStatus(Status status);
    }
}
