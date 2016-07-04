using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class BreakableSprite : SpriteList
    {
        public void SetState(double percent)
        {
            int count = this.Positions.Count();
            int pos = (int)Math.Min(Math.Floor((1-percent) * count), count - 1);
            this.SourceRectangle = Positions[pos];
        }

        public override Sprite Clone()
        {
            return new BreakableSprite()
            {
                Texture = this.Texture,
                SourceRectangle = this.SourceRectangle,
                Positions = this.Positions
            };
        }
    }
}
