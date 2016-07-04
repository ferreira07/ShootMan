using GameEngine;
using Microsoft.Xna.Framework;
using GameEngine.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class CharacterFactory
    {
        public static Character CreateCharacter(ECharacterType type, Vector2 position, IController controller)
        {
            Character ret = null;

            switch (type)
            {
                case ECharacterType.Fulano:
                    ret = new Character(Sprites.GetSprite(ESpriteType.char1), controller);
                    ret.Position = position - new Vector2(16,16);
                    ret.SetDrawPosition(0, -16);
                    ret.SetSize(32, 32);
                    ret.UpdateRectangle();
                    ret.Hp = 100;
                    ret.MaxSpeed = Constants.SpeedBase;
                    break;
                case ECharacterType.Beltrano:
                    ret = new Character(Sprites.GetSprite(ESpriteType.char2), controller);
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(0, -16);
                    ret.SetSize(32, 32);
                    ret.UpdateRectangle();
                    ret.Hp = 80;
                    ret.MaxSpeed = Constants.SpeedBase * 1.1f;
                    break;
                case ECharacterType.Siclano:
                    ret = new Character(Sprites.GetSprite(ESpriteType.char3), controller);
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(0, -16);
                    ret.SetSize(32, 32);
                    ret.UpdateRectangle();
                    ret.Hp = 120;
                    ret.MaxSpeed = Constants.SpeedBase * 0.9f;
                    break;
            }

            return ret;
        }
    }
}
