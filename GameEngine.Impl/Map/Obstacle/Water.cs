using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using GameEngine.Draw;
using Microsoft.Xna.Framework;

namespace GameEngine.Impl.Map.Obstacle
{
    public class Water : MapObject
    {
        public Water(Sprite sprite, RectangleF position)
        {
            Sprite = sprite;
            DrawRectangle = position.ToRectangle();
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y- position.Height);
        }

        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground;
            }
        }

        public override EColisionType ColisionType
        {
            get
            {
                return EColisionType.Blocking;
            }
        }

        public override void Damage(int ammount)
        {
            // Faz nada
        }
    }
}
