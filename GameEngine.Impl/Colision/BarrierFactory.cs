using GameEngine.Colision;
using GameEngine.Draw;
using GameEngine.Map;
using GameEngine.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Colision
{
    public class BarrierFactory : IBarrierFactory
    {
        public IMapObject CreateBarrier(EBarrierType type, Rectangle position)
        {
            IMapObject ret = null;
            switch (type)
            {
                case EBarrierType.BasicBarrier:
                    ret = new Barrier(position, 50, Sprites.GetSprite(ESpriteType.barrier11));
                    break;
                case EBarrierType.Box:
                    ret = new Box(position, 10, Sprites.GetSprite(ESpriteType.Crate), RandomPowerUp());
                    break;
                case EBarrierType.Wall:
                    ret = new Wall(Sprites.GetSprite(ESpriteType.barrier11), position);
                    break;
                case EBarrierType.Stone:
                    ret = new MovableBarrier(position, 50, (TimeChangeSprite)Sprites.GetSprite(ESpriteType.EmergingStone), Sprites.GetSprite(ESpriteType.RollingStone))
                    {
                        DamageAmmount = 20,
                        MaxSpeed = Constants.SpeedBase * 1.5f
                    };
                    break;
            }
            return ret;
        }

        static Random Random = new Random();
        private static EPowerUpType RandomPowerUp()
        {
            List<EPowerUpType> powerUps = new List<EPowerUpType>()
            {
                EPowerUpType.Hp,
                EPowerUpType.Mp,
                EPowerUpType.Speed
            };

            return powerUps[Random.Next(powerUps.Count)];
        }
    }
}
