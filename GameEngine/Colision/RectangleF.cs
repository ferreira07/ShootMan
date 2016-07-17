using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Colision
{
    public struct RectangleF
    {
        public RectangleF(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public RectangleF(Vector2 position, int width, int height) : this()
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }
        #region Properties
        public float X { get; set; }
        public float Y { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public Point Center { get { return new Point((int)(X + Width/2), (int)(Y + Height/2)); } }
        #endregion

        public bool Intersects(RectangleF other)
        {
            bool intersectX = IntersectsAxis(X, other.X, Width, other.Width);
            bool intersectY = IntersectsAxis(Y, other.Y, Height, other.Height);

            return intersectX && intersectY;
        }
        private static bool IntersectsAxis(float p1, float p2, float l1, float l2)
        {
            return !(p1 + l1 <= p2 || p2 + l2 <= p1);
        }
        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
    }
}
