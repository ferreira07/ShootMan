using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public virtual Sprite Clone()
        {
            return new Sprite()
            {
                Texture = this.Texture,
                SourceRectangle = this.SourceRectangle
            };
        }
    }
}
