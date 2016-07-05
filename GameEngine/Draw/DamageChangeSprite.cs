using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class DamageChangeSprite : SpriteList, IDamageChangeSprite
    {
        public override ESpriteChangeType SpriteChangeType
        {
            get { return ESpriteChangeType.Damage; }
        }

        public void SetDamagePercent(double value)
        {
            int count = this.Positions.Count();
            int pos = (int)Math.Min(Math.Floor((value) * count), count - 1);
            this.SourceRectangle = Positions[pos];
        }

        public override Sprite Clone()
        {
            return new DamageChangeSprite()
            {
                Texture = this.Texture,
                SourceRectangle = this.SourceRectangle,
                Positions = this.Positions
            };
        }
    }
}
