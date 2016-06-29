using ShootMan.Colision;
using ShootMan.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Move
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

        public override void OnColide(IColider c)
        {
            if(c is Wall)
            {
                Map.Remove(this);
            }
            if (c is Character || c is Barrier)
            {
                //TODO calcular Dano
                c.Damage(DamageAmmount);
                Piercy--;
                if(Piercy < 0)
                    Map.Remove(this);
            }
        }
    }
}
