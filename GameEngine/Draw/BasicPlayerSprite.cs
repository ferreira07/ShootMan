using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Draw
{
    public class BasicPlayerSprite : SpriteList2, IFacingChangeSprite, IMoveChangeSprite
    {
        public BasicPlayerSprite()
        {

        }
        public BasicPlayerSprite(Texture2D texture, int w = 32, int h = 48)
        {
            Texture = texture;

            FacingIndexer = GenerateFacingIndexer();
            MoveCicle = 40;

            int dirCount = 4;
            int moveCount = 4;
            Positions = GeneratePositions(w, h, dirCount, moveCount);
            this.SourceRectangle = Positions[facingIndex][movingIndex];
        }

        private static Rectangle[][] GeneratePositions(int w, int h, int dirCount, int moveCount)
        {
            Rectangle[][]  positions = new Rectangle[dirCount][];
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

        private static Dictionary<Vector2, int> GenerateFacingIndexer()
        {
            Dictionary<Vector2, int>  facingIndexer = new Dictionary<Vector2, int>();
            facingIndexer.Add(new Vector2(0, -1), 0);
            facingIndexer.Add(new Vector2(-1, 0), 1);
            facingIndexer.Add(new Vector2(1, 0), 2);
            facingIndexer.Add(new Vector2(0, 1), 3);
            return facingIndexer;
        }

        public override ESpriteChangeType SpriteChangeType
        {
            get { return ESpriteChangeType.Facing | ESpriteChangeType.Move; }
        }

        public Dictionary<Vector2, int> FacingIndexer { get; set; }

        public float MoveCicle { get; set; }
        public float Moved { get; set; }

        private int facingIndex;
        private int movingIndex;

        public void Move(float v)
        {
            Moved += v;
            if (Moved > MoveCicle)
            {
                Moved = 0;
            }
            int count = this.Positions[facingIndex].Count();
            movingIndex = (int)Math.Min(Math.Floor((Moved / MoveCicle) * count), count - 1);
            this.SourceRectangle = Positions[facingIndex][movingIndex];
        }

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
                //Se mudar de direção, reinicia o movimento
                Moved = 0;
                this.SourceRectangle = Positions[facingIndex][movingIndex];
            }
        }

        public override Sprite Clone()
        {
            return new BasicPlayerSprite()
            {
                Texture = this.Texture,
                Positions = this.Positions,
                FacingIndexer = this.FacingIndexer,
                MoveCicle = this.MoveCicle,
                SourceRectangle = this.SourceRectangle
            };
        }
    }
}
