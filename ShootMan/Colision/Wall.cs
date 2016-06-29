using ShootMan.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShootMan.Map;

namespace ShootMan.Colision
{
    public class Wall : MapObject
    {
        public Wall(Sprite sprite, Rectangle position)
        {
            Sprite = sprite;
            DrawRectangle = position;
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
        }

        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground | EColisionLayer.Floating;
            }
        }

        public override EColisionType ColisionType { get { return EColisionType.Blocking; } }

        public override void Damage(int ammount)
        {
            // Faz nada
        }
    }
}
