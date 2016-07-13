using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using Microsoft.Xna.Framework;
using GameEngine.Draw;

namespace GameEngine.Player
{
    public class PowerUp : MapObject, IPowerUp
    {
        public EPowerUpType PowerUpType { get; set; }

        public PowerUp(RectangleF position, Sprite sprite, EPowerUpType powerUpType)
        {
            Sprite = sprite;
            DrawRectangle = position.ToRectangle();
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
            PowerUpType = powerUpType;
        }

        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground | EColisionLayer.Floating;
            }
        }

        public override EColisionType ColisionType
        {
            get
            {
                return EColisionType.Hit;
            }
        }

        public override void Damage(int ammount)
        {
            throw new NotImplementedException();
        }

        public void Aplly(Character c)
        {
            Remove();
            switch (this.PowerUpType)
            {
                case EPowerUpType.Hp:
                    c.SetHp(c.MaxHp + 10);
                    break;
                case EPowerUpType.Mp:
                    c.SetMp(c.MaxMp + 10, c.MpRechargeSpeed + 2);
                    break;
                case EPowerUpType.Speed:
                    c.MaxSpeed = c.MaxSpeed + Constants.SpeedBase * 0.1f;
                    break;
            }
        }
    }
}
