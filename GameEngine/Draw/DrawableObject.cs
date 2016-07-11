using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Colision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class DrawableObject
    {
        private static int maxId = 1;
        private int _Id = maxId++;

        public int Id { get { return _Id; } }
        
        public Vector2 Position { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int Dx { get; set; }

        public int Dy { get; set; }

        public Sprite Sprite { get; set; }

        public Rectangle DrawRectangle { get; set; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
#if Debuging
#endif
            if (this is IColider)
            {
                Sprite s = Sprites.GetSprite(ESpriteType.ColisionArea);
                spriteBatch.Draw(s.Texture, (this as IColider).ColisionRectangle, s.SourceRectangle, Color.White);
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
            Dx = dx;
            Dy = dy;
        }


        public override bool Equals(object obj)
        {
            return obj is DrawableObject ? Equals(obj as DrawableObject) : false;
        }
        public bool Equals(DrawableObject obj)
        {
            return obj.Id == Id;
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
