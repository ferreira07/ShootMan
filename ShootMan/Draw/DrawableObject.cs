using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShootMan.Colision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Draw
{
    public class DrawableObject
    {
        private static int maxId = 1;
        private int _Id = maxId++;

        public int Id { get { return _Id; } }

        public Vector2 Position { get; set; }

        public int Width { get; internal set; }
        public int Height { get; internal set; }

        public int Dx { get; set; }

        public int Dy { get; set; }

        public Sprite Sprite { get; set; }

        public Rectangle DrawRectangle { get; set; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this is IColider)
            {
                spriteBatch.Draw(ShootMan.textureBLUE, (this as IColider).ColisionRectangle, Color.White);
            }
            spriteBatch.Draw(Sprite.Texture, DrawRectangle, Sprite.SourceRectangle, Color.White);
        }

        public void SetSize(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public void SetDrawPosition(int dx, int dy)
        {
            Dx = 0;
            Dy = -16;
        }
    }
}
