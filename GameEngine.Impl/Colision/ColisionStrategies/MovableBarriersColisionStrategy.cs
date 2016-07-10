using GameEngine.Colision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Colision.ColisionStrategies
{
    public class MovableBarriersColisionStrategy: IColisionStrategy
    {
        public MovableBarrier MovableBarrier1 { get; set; }
        public MovableBarrier MovableBarrier2 { get; set; }

        public MovableBarriersColisionStrategy(MovableBarrier movableBarrier1, MovableBarrier movableBarrier2)
        {
            MovableBarrier1 = movableBarrier1;
            MovableBarrier2 = movableBarrier2;
        }
        public void ProcessColision()
        {
            MovableBarrier2.Remove();
            MovableBarrier1.Remove();
        }
    }
}
