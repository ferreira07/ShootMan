using GameEngine.Colision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Move
{
    public class MoveTo
    {
        public static Vector2 Move(float x, float y, RectangleF ColisionRectangle, RectangleF item)
        {
            x = RepositionAxis(x, ColisionRectangle.X, ColisionRectangle.Width, item.X, item.Width);

            y = RepositionAxis(y, ColisionRectangle.Y, ColisionRectangle.Height, item.Y, item.Height);

            return new Vector2(x, y);
        }

        public static float RepositionAxis(float v, float p1, float s1, float p2, float s2)
        {
            if (v > 0 && //Velocidade maior que 0
                p1 + s1 <= p2 && //Antes não havia colisão
                p1 + s1 + v > p2) //Passou a haver colisão
            {
                v = p2 - s1 - p1;
            }
            else if (v < 0 && //Velocidade menor que 0
                p2 + s2 <= p1 && //Antes não havia colisão
                p2 + s2  > p1 + v) //Passou a haver colisão
            {
                v = p2 + s2 - p1;
            }

            return v;
        }
    }
}
