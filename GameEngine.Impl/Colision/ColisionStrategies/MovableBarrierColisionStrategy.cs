using GameEngine.Colision;
using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Colision.ColisionStrategies
{
    public class MovableBarrierColisionStrategy : IColisionStrategy
    {
        public MovableBarrier MovableBarrier { get; set; }
        public IColider Colider { get; set; }

        public MovableBarrierColisionStrategy(MovableBarrier movableBarrier, IColider colider)
        {
            MovableBarrier = movableBarrier;
            Colider = colider;
        }
        public void ProcessColision()
        {
            if (Colider is IDefensesContainer)
                MovableBarrier.Map.DoDamage(MovableBarrier, Colider as IDefensesContainer);
            MovableBarrier.Remove();
        }
    }
}
