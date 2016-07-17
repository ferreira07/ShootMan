using GameEngine.Colision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Util
{
    public static class CreateObjectHelper
    {
        public static Vector2 GetStartPosition(Vector2 dir, RectangleF creatorRect, int width, int height)
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
