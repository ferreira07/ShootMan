using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Draw
{
    public class DirectionTimeChangeSprite : SpriteList2, IFacingChangeSprite, ITimeChangeSprite
    {
        public DirectionTimeChangeSprite()
        {

        }

        public TimeSpan TimeCicle { get; set; }

        private TimeSpan TimeCount;

        public DirectionTimeChangeSprite(Texture2D texture, int w = 32, int h = 48, int moveCount = 4, int directionType = 0)
        {
            Texture = texture;

            FacingIndexer = GenerateFacingIndexer(directionType);
            this.TimeCicle = TimeSpan.FromSeconds(0.5f);

            int dirCount = 4;
            Positions = GeneratePositions(w, h, dirCount, moveCount);
            this.SourceRectangle = Positions[facingIndex][movingIndex];
        }

        private static Rectangle[][] GeneratePositions(int w, int h, int dirCount, int moveCount)
        {
            Rectangle[][] positions = new Rectangle[dirCount][];
            for (int i = 0; i < dirCount; i++)
            {
                positions[i] = new Rectangle[moveCount];
                for (int j = 0; j < moveCount; j++)
                {
                    positions[i][j] = new Rectangle(j * w, i * h, w, h);
                }
            }
            return positions;
        }

        private static Dictionary<Vector2, int> GenerateFacingIndexer(int directionType)
        {
            Dictionary<Vector2, int> facingIndexer = new Dictionary<Vector2, int>();
            if (directionType == 0)
            {
                facingIndexer.Add(new Vector2(0, -1), 0);
                facingIndexer.Add(new Vector2(-1, 0), 1);
                facingIndexer.Add(new Vector2(1, 0), 2);
                facingIndexer.Add(new Vector2(0, 1), 3);
            }
            else
            {
                facingIndexer.Add(new Vector2(0, 1), 0);
                facingIndexer.Add(new Vector2(1, 0), 1);
                facingIndexer.Add(new Vector2(0, -1), 2);
                facingIndexer.Add(new Vector2(-1, 0), 3);
            }
            return facingIndexer;
        }

        public override ESpriteChangeType SpriteChangeType
        {
            get { return ESpriteChangeType.Facing | ESpriteChangeType.Time; }
        }

        public Dictionary<Vector2, int> FacingIndexer { get; set; }

        public bool AnimateOnce { get; set; }

        public bool StopAnimate { get; set; }

        private int facingIndex, movingIndex;
        
        public void SetFacing(Vector2 direction)
        {
            direction.Normalize();
            float minDistance = float.MaxValue;
            int index = -1;
            foreach (var item in FacingIndexer)
            {
                float d = Vector2.Distance(direction, item.Key);
                if (d < minDistance)
                {
                    minDistance = d;
                    index = item.Value;
                }
            }
            if (index >= 0 && index != facingIndex)
            {
                facingIndex = index;
                TimeCount = TimeSpan.Zero;
                movingIndex = 0;
                this.SourceRectangle = Positions[facingIndex][movingIndex];
            }
        }

        public override Sprite Clone()
        {
            return new DirectionTimeChangeSprite()
            {
                Texture = this.Texture,
                Positions = this.Positions,
                FacingIndexer = this.FacingIndexer,
                TimeCicle = this.TimeCicle,
                SourceRectangle = this.SourceRectangle
            };
        }

        public void PassTime(TimeSpan time)
        {
            TimeCount += time;
            if (TimeCount > TimeCicle)
            {
                TimeCount = TimeSpan.Zero;
            }
            int count = this.Positions[facingIndex].Count();
            movingIndex = (int)Math.Min((int)Math.Floor((TimeCount.TotalMilliseconds / TimeCicle.TotalMilliseconds) * count), count - 1);
            this.SourceRectangle = Positions[facingIndex][movingIndex];
        }
    }
}
