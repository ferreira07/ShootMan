using GameEngine;
using Microsoft.Xna.Framework;
using GameEngine.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Move;
using GameEngine.Impl.Colision;
using GameEngine.Impl.Player.Actions;

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
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(-4, -21);
                    ret.SetSize(25, 25);
                    ret.UpdateRectangle();
                    ret.SetHp(100);
                    ret.SetMp(50, 15);
                    ret.AddAction(new ProjectilAction(EControllerAction.Release1, 10, EProjectilType.Bullet, TimeSpan.FromSeconds(0.2)));
                    ret.AddAction(new ProjectilAction(EControllerAction.Release2, 20, EProjectilType.Fireball, TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.2), TimeSpan.FromSeconds(0.2)));
                    ret.AddAction(new ProjectilAction(EControllerAction.Release3, 50, EProjectilType.ChargedBullet, TimeSpan.FromSeconds(1)));
                    ret.MaxSpeed = Constants.SpeedBase;
                    ret.Defenses = new Map.Defenses();
                    break;
                case ECharacterType.Beltrano:
                    ret = new Character(Sprites.GetSprite(ESpriteType.char2), controller);
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(-4, -21);
                    ret.SetSize(25, 25);
                    ret.UpdateRectangle();
                    ret.SetHp(80);
                    ret.SetMp(50, 15);
                    ret.AddAction(new ProjectilAction(EControllerAction.Release1, 10, EProjectilType.Bullet, TimeSpan.FromSeconds(0.2)));
                    ret.AddAction(new ProjectilAction(EControllerAction.Release2, 20, EProjectilType.Fireball, TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.2), TimeSpan.FromSeconds(0.2)));
                    ret.AddAction(new MoveObjectAction(EControllerAction.Release3, 10, TimeSpan.FromSeconds(1)));
                    ret.AddAction(new CreateBarrierAction(EControllerAction.Release4, 30, new BarrierFactory(), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(0.2), TimeSpan.Zero));
                    ret.MaxSpeed = Constants.SpeedBase * 1.1f;
                    ret.Defenses = new Map.Defenses();
                    break;
                case ECharacterType.Siclano:
                    ret = new Character(Sprites.GetSprite(ESpriteType.char3), controller);
                    ret.Position = position - new Vector2(16, 16);
                    ret.SetDrawPosition(-4, -21);
                    ret.SetSize(25, 25);
                    ret.UpdateRectangle();
                    ret.SetHp(120);
                    ret.SetMp(50, 12);
                    ret.AddAction(new ProjectilAction(EControllerAction.Release1, 10, EProjectilType.Bullet, TimeSpan.FromSeconds(0.2)));
                    ret.AddAction(new ProjectilAction(EControllerAction.Release2, 20, EProjectilType.Fireball, TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.2), TimeSpan.FromSeconds(0.2)));
                    ret.AddAction(new ProjectilAction(EControllerAction.Release3, 50, EProjectilType.ChargedBullet, TimeSpan.FromSeconds(1)));
                    ret.MaxSpeed = Constants.SpeedBase * 0.9f;
                    ret.Defenses = new Map.Defenses();
                    break;
            }

            return ret;
        }
    }
}
