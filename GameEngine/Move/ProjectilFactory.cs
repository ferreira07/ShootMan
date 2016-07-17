using GameEngine;
using Microsoft.Xna.Framework;
using GameEngine.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using GameEngine.Combat;
using GameEngine.Util;

namespace GameEngine.Move
{
    public class ProjectilFactory
    {
        public static Projectil Create(EProjectilType type, Vector2 direction, RectangleF CreatorRectangle)
        {
            Projectil p = new Projectil();
            direction.Normalize();

            switch (type)
            {
                case EProjectilType.Bullet:
                    p.Width = 10;
                    p.Height = 10;
                    p.Sprite = Sprites.GetSprite(ESpriteType.Bullet);
                    p.DrawRectangle = p.Sprite.SourceRectangle;
                    p.SetSize(10, 10);
                    p.Speed = direction * (Constants.SpeedBase * 2.5f);
                    p.Attack = new Attack(10, EDamageType.Physical);
                    break;
                case EProjectilType.ChargedBullet:
                    p.Width = 20;
                    p.Height = 20;
                    p.Sprite = Sprites.GetSprite(ESpriteType.Bullet2);
                    p.DrawRectangle = p.Sprite.SourceRectangle;
                    p.SetSize(20, 20);
                    p.Speed = direction * (Constants.SpeedBase * 5f);
                    p.Attack = new Attack(20, EDamageType.Physical);
                    break;
                case EProjectilType.Fireball:
                    p.Width = 16;
                    p.Height = 16;
                    p.Sprite = Sprites.GetSprite(ESpriteType.Fireball);
                    p.DrawRectangle = p.Sprite.SourceRectangle;
                    p.SetSize(16, 16);
                    p.Speed = direction * (Constants.SpeedBase * 2f);
                    p.Attack = new Attack(25, EDamageType.Fire);
                    break;
                case EProjectilType.PoisonBullet:
                    p.Width = 10;
                    p.Height = 10;
                    p.Sprite = Sprites.GetSprite(ESpriteType.Bullet3);
                    p.DrawRectangle = p.Sprite.SourceRectangle;
                    p.SetSize(10, 10);
                    p.Speed = direction * (Constants.SpeedBase * 2.5f);
                    p.Attack = new Attack(10, EDamageType.Poison)
                    {
                        Status = new Status(EStatusType.Poison, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1))
                        {
                            TimeCicleAttack = new Attack(1, EDamageType.Poison)
                        }
                    };
                    break;
            }
            p.Position = CreateObjectHelper.GetStartPosition(direction, CreatorRectangle, p.Width, p.Height);
            p.UpdateRectangle();
            return p;
        }
                
    }
}
