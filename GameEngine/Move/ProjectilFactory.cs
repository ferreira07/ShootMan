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
            }
            p.Position = _GetStartPosition(direction, CreatorRectangle, p.Width, p.Height);
            p.UpdateRectangle();
            return p;
        }

        private static Vector2 _GetStartPosition(Vector2 dir, RectangleF creatorRect, int width, int height)
        {
            float x = 0, y = 0;
            float cx = creatorRect.X + creatorRect.Width / 2 - width / 2;
            float cy = creatorRect.Y + creatorRect.Height / 2 - height / 2;
            float proportionCreator = creatorRect.Width / creatorRect.Height;
            if (Math.Abs(dir.X) > Math.Abs(dir.Y) * proportionCreator)
            {
                //Borda horizontal
                if (dir.X < 0)
                {
                    x = creatorRect.X - width;
                }
                else
                {
                    x = creatorRect.X + creatorRect.Width + 1;
                }
                y = cy + ((cx - x) * dir.Y) / dir.X;
            }
            else
            {
                //Borda Lateral
                if (dir.Y > 0)
                {
                    y = creatorRect.Y - height;
                }
                else
                {
                    y = creatorRect.Y + creatorRect.Height + 1;
                }
                x = cx + ((cy - y) * dir.X) / dir.Y;
            }
            return new Vector2(x, y);
        }
    }
}
