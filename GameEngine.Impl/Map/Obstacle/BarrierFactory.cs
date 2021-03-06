﻿using GameEngine.Colision;
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

        public List<Sprite> BarrierSprites { get; set; }
        public List<Sprite> BoxSprites { get; set; }
        public List<Sprite> WallSprites { get; set; }

        public IMapObject CreateBarrier(EBarrierType type, RectangleF position)
        {
            MapObject ret = null;
            switch (type)
            {
                case EBarrierType.BasicBarrier:
                    ret = new Barrier(position, 50, _GetRandom(BarrierSprites))
                    {
                        Defenses = new Defenses()
                    };
                    break;
                case EBarrierType.Box:
                    ret = new Box(position, 10, _GetRandom(BoxSprites), RandomPowerUp())
                    {
                        Defenses = new Defenses()
                    };
                    break;
                case EBarrierType.Wall:
                    ret = new Wall(_GetRandom(WallSprites), position)
                    {
                        Defenses = new Defenses()
                    };
                    break;
                case EBarrierType.Water:
                    ret = new Water(Sprites.GetSprite(ESpriteType.Water), position)
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
            ret.Defenses.AddResistance(EDamageType.Poison, 0.1f);
            return ret;
        }

        private Sprite _GetRandom(List<Sprite> sprites)
        {
            return sprites[Random.Next(sprites.Count)];
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
