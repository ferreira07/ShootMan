﻿using Microsoft.Xna.Framework;
using GameEngine.Colision;
using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Draw;

namespace GameEngine.Move
{
    public abstract class MovingObject : MapObject, IUpdateableObject
    {
        public float MaxSpeed { get; set; }

        public Vector2 Speed { get; set; }

        public void UpdateRectangle()
        {
            DrawRectangle = new Rectangle((int)Position.X + Dx, (int)Position.Y + Dy, DrawRectangle.Width, DrawRectangle.Height);
            ColisionRectangle = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public virtual void Update(TimeSpan elapsedGameTime)
        {
            Map.Move(this, elapsedGameTime);
        }
    }
}
