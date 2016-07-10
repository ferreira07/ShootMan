using GameEngine.Colision;
using GameEngine.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Move
{
    public class Projectil : MovingObject
    {
        public int Piercy { get; set; }

        public override EColisionType ColisionType
        {
            get { return EColisionType.Hit; }
        }

        public int DamageAmmount { get; internal set; }

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
    }
}
