using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Move
{
    public class ProjectilFactory
    {
        public static Projectil Create(EProjectilType type, Vector2 direction, Rectangle CreatorRectangle)
        {
            Projectil p = new Projectil();
            direction.Normalize();

            switch (type)
            {
                case EProjectilType.Bullet:
                    p.Width = 10;
                    p.Height = 10;
                    p.Sprite = new Draw.Sprite() { SourceRectangle = new Rectangle(0, 0, p.Width, p.Height), Texture = ShootMan.BulletTexture };
                    p.Speed = direction * (ShootMan.SpeedBase * 2.5f);
                    p.DamageAmmount = 10;
                    break;
                case EProjectilType.ChargedBullet:
                    p.Width = 20;
                    p.Height = 20;
                    p.Sprite = new Draw.Sprite() { SourceRectangle = new Rectangle(0, 0, p.Width, p.Height), Texture = ShootMan.BulletTexture };
                    p.Speed = direction * (ShootMan.SpeedBase * 5f);
                    p.DamageAmmount = 20;
                    break;
            }
            p.Position = _GetStartPosition(direction, CreatorRectangle, p.Width, p.Height);
            p.UpdateRectangle();
            return p;
        }

        private static Vector2 _GetStartPosition(Vector2 dir, Rectangle creatorRect, int width, int height)
        {
            float x = 0,y = 0;
            int cx = creatorRect.X + creatorRect.Width / 2 - width / 2;
            int cy = creatorRect.Y + creatorRect.Height / 2 - height / 2;
            float proportionCreator = creatorRect.Width / creatorRect.Height;
            if (Math.Abs(dir.X)  > Math.Abs(dir.Y) * proportionCreator)
            {
                //Borda horizontal
                if(dir.X < 0)
                {
                    x = creatorRect.X - width;
                }
                else
                {
                    x = creatorRect.X  + creatorRect.Width + 1;
                }
                y = cy + ((cx - x) * dir.Y)/dir.X;
            }else
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
