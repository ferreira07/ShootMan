using Microsoft.Xna.Framework;
using ShootMan.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Colision
{
    public class BarrierFactory
    {
        public static Barrier CreateBarrier(EBarrierType type, Rectangle position)
        {
            Barrier ret = new Barrier(position, 50, Sprites.GetSprite(ESprite.barrier11), Sprites.GetSprite(ESprite.barrier12));
            
            return ret;
        }
    }
}
