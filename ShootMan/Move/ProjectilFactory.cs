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
        public static Projectil Create(EProjectilType type, Vector2 direction, Vector2 position)
        {
            Projectil p = new Projectil();
            direction.Normalize();

            switch (type)
            {
                case EProjectilType.Bullet:
                    p.Sprite = new Draw.Sprite() { SourceRectangle = new Rectangle(0, 0, 10, 10), Texture = ShootMan.BulletTexture };
                    p.Position = position;
                    p.ColisionRectangle = new Rectangle((int)position.X, (int)position.Y, 10, 10);
                    p.Speed = direction * ShootMan.SpeedBase * 2;
                    break;
            }
            return p;
        }
    }
}
