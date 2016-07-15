using GameEngine.Colision;
using GameEngine.Combat;
using GameEngine.Draw;
using GameEngine.Map;
using GameEngine.Map.Obstacle;
using GameEngine.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Map.Obstacle
{
    public class BarrierFactory : IBarrierFactory
    {
        public IMapObject CreateBarrier(EBarrierType type, RectangleF position)
        {
            IMapObject ret = null;
            switch (type)
            {
                case EBarrierType.BasicBarrier:
                    ret = new Barrier(position, 50, Sprites.GetSprite(ESpriteType.barrier11))
                    {
                        Defenses = new Defenses()
                    };
                    break;
                case EBarrierType.Box:
                    ret = new Box(position, 10, Sprites.GetSprite(ESpriteType.Crate), RandomPowerUp())
                    {
                        Defenses = new Defenses()
                    };
                    break;
                case EBarrierType.Wall:
                    ret = new Wall(Sprites.GetSprite(ESpriteType.barrier11), position)
                    {
                        Defenses = new Defenses()
                    };
                    break;
                case EBarrierType.Stone:
                    ret = new MovableBarrier(position, 50, (TimeChangeSprite)Sprites.GetSprite(ESpriteType.EmergingStone), Sprites.GetSprite(ESpriteType.RollingStone))
                    {
                        Attack = new Attack(20, EDamageType.Physical),
                        MaxSpeed = Constants.SpeedBase * 1.5f,
                        Defenses = new Defenses()
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
