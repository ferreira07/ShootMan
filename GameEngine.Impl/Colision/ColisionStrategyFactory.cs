using GameEngine.Colision;
using GameEngine.Impl.Colision.ColisionStrategies;
using GameEngine.Move;
using GameEngine.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Colision
{
    public class ColisionStrategyFactory : IColisionStrategyFactory
    {
        public IColisionStrategy Create(IColider item1, IColider item2)
        {
            IColisionStrategy ret = null;
            switch (item1.ColisionType)
            {
                case EColisionType.Blocking:
                    switch (item2.ColisionType)
                    {
                        case EColisionType.Blocking:
                            //Não deveria acontecer
                            break;
                        case EColisionType.Hit:
                            ret = ColideBlockingHit(item1, item2);
                            break;
                    }
                    break;
                case EColisionType.Hit:
                    switch (item2.ColisionType)
                    {
                        case EColisionType.Blocking:
                            ret = ColideBlockingHit(item2, item1);
                            break;
                        case EColisionType.Hit:
                            ret = ColideHitHit(item1, item2);
                            break;
                    }
                    break;
            }
            return ret;
        }

        private IColisionStrategy ColideBlockingHit(IColider blocking, IColider hit)
        {
            IColisionStrategy ret = null;

            if (hit is Projectil)
            {
                ret = new ProjectilColisionStrategy(hit as Projectil, blocking);
            }
            else if (hit is MovableBarrier)
            {
                ret = new MovableBarrierColisionStrategy(hit as MovableBarrier, blocking);                
            }
            else if (hit is IPowerUp && blocking is Character)
            {
                ret = new PowerUpCharacterColisionStrategy(hit as IPowerUp, blocking as Character);
            }
            return ret;
        }

        private IColisionStrategy ColideHitHit(IColider hit1, IColider hit2)
        {
            return null;
        }
    }
}
