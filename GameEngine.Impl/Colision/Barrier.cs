using Microsoft.Xna.Framework;
using GameEngine.Draw;
using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;

namespace GameEngine.Impl.Colision
{
    public class Barrier : MapObject
    {
        public Barrier(RectangleF position, int hp, Sprite sprite)
        {
            Sprite = sprite;
            DrawRectangle = position.ToRectangle();
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
            SetHp(hp);
        }
        
        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground | EColisionLayer.Floating;
            }
        }

        public override EColisionType ColisionType { get { return EColisionType.Blocking; } }              
        
    }
}
