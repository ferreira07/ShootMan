using GameEngine.Draw;
using GameEngine.Map;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class PowerUpFactory
    {
        public IMapObject CreatePowerUp(EPowerUpType type, Rectangle position)
        {
            PowerUp ret = null;
            switch (type)
            {
                case EPowerUpType.Hp:
                    ret = new PowerUp(position, Sprites.GetSprite(ESpriteType.RedGem), type);
                    break;
                case EPowerUpType.Mp:
                    ret = new PowerUp(position, Sprites.GetSprite(ESpriteType.BlueGem), type);
                    break;
                case EPowerUpType.Speed:
                    ret = new PowerUp(position, Sprites.GetSprite(ESpriteType.GreenGem), type);
                    break;
            }
            return ret;
        }
    }
}
