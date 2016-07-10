using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class TimeChangeSprite : SpriteList, ITimeChangeSprite
    {
        public TimeSpan TimeCicle { get; set; }
        private TimeSpan TimeCount;
        public override ESpriteChangeType SpriteChangeType
        {
            get { return ESpriteChangeType.Time; }
        }
        public bool AnimateOnce { get; set; }
        public bool StopAnimate { get; set; }

        public TimeChangeSprite()
        {

        }

        public TimeChangeSprite(Texture2D texture, Rectangle[] positions, float timeSpanSeconds)
        {
            this.Texture = texture;
            this.Positions = positions;
            this.TimeCicle = TimeSpan.FromSeconds(timeSpanSeconds);
            TimeCount = TimeSpan.Zero;
            this.SourceRectangle = Positions[0];
        }
        public void PassTime(TimeSpan time)
        {
            if (!StopAnimate)
            {
                TimeCount += time;
                if (TimeCount > TimeCicle)
                {
                    if (AnimateOnce)
                    {
                        TimeCount = TimeCicle;
                        StopAnimate = true;
                    }
                    else
                    {
                        TimeCount = TimeSpan.Zero;
                    }
                }
                int count = this.Positions.Count();
                int index = (int)Math.Min(Math.Floor((TimeCount.TotalMilliseconds / TimeCicle.TotalMilliseconds) * count), count - 1);
                this.SourceRectangle = Positions[index];
            }
        }
        public override Sprite Clone()
        {
            return new TimeChangeSprite()
            {
                Texture = this.Texture,
                SourceRectangle = this.SourceRectangle,
                Positions = this.Positions,
                TimeCicle = this.TimeCicle
            };
        }
    }
}
