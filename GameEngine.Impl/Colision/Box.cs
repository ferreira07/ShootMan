using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using GameEngine.Draw;
using Microsoft.Xna.Framework;
using GameEngine.Player;

namespace GameEngine.Impl.Colision
{
    public class Box : Barrier
    {
        public EPowerUpType PowerUp { get; set; }

        public Box(Rectangle position, int hp, Sprite sprite, EPowerUpType powerUp)
            : base(position, hp, sprite)
        {
            PowerUp = powerUp;
        }

        protected override void OnZeroHp()
        {
            Remove();
            PowerUpFactory factory = new PowerUpFactory();
            Map.Add(factory.CreatePowerUp(this.PowerUp, new Rectangle((int)Position.X+10, (int)Position.Y + 10, 14,14)));
        }
    }
}
