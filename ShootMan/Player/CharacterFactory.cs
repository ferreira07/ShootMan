using Microsoft.Xna.Framework;
using ShootMan.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Player
{
    public class CharacterFactory
    {
        public static Character CreateCharacter(ECharacterType type, Vector2 position, IController controller)
        {
            Character ret = null;

            switch (type)
            {
                case ECharacterType.Fulano:
                    ret = new Character(Sprites.GetSprite(ESprite.char1), controller);
                    ret.Position = position - new Vector2(16,16);
                    ret.SetDrawPosition(0, 16);
                    ret.SetSize(32, 32);
                    ret.UpdateRectangle();
                    ret.Hp = 100;
                    ret.MaxSpeed = ShootMan.SpeedBase;
                    break;
                case ECharacterType.Beltrano:
                    ret = new Character(Sprites.GetSprite(ESprite.char2), controller);
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(0, 16);
                    ret.SetSize(32, 32);
                    ret.UpdateRectangle();
                    ret.Hp = 80;
                    ret.MaxSpeed = ShootMan.SpeedBase * 1.1f;
                    break;
                case ECharacterType.Siclano:
                    ret = new Character(Sprites.GetSprite(ESprite.char3), controller);
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(0, 16);
                    ret.SetSize(32, 32);
                    ret.UpdateRectangle();
                    ret.Hp = 120;
                    ret.MaxSpeed = ShootMan.SpeedBase * 0.9f;
                    break;
            }

            return ret;
        }
    }
}
