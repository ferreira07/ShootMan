using ShootMan.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootMan.Colision
{
    public class Wall : DrawableObject, IColider, IMapObject
    {
        public Wall(Sprite sprite, Rectangle position)
        {
            Sprite = sprite;
            DrawRectangle = position;
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
        }

        public EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground | EColisionLayer.Floating;
            }            
        }

        public Rectangle ColisionRectangle { get; set; }

        public EColisionType ColisionType { get { return EColisionType.Blocking; } }

        public void Damage(int ammount)
        {
            //Não faz nada
        }

        public Map Map { get; set; }
    }
}
