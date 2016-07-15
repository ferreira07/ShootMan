using GameEngine.Colision;
using GameEngine.Combat;
using GameEngine.Map;
using GameEngine.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Move
{
    public class Projectil : MovingObject, IAttackContainer
    {
        public int Piercy { get; set; }

        public override EColisionType ColisionType
        {
            get { return EColisionType.Hit; }
        }

        public Attack Attack { get; internal set; }

        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Floating;
            }
        }

        public override void Damage(int ammount)
        {
            throw new NotImplementedException();
        }

        public Attack GetAttack()
        {
            return Attack;
        }
    }
}
