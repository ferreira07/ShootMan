using GameEngine.Colision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class CreateBarrierAction : Action
    {
        public CreateBarrierAction(EControllerAction action, int mpCost, IBarrierFactory factory, TimeSpan exaustingActionTime, TimeSpan exaustingMoveTime, TimeSpan chargingTime)
            : base(action, mpCost, exaustingActionTime, exaustingMoveTime, chargingTime)
        {
            Factory = factory;
        }

        public IBarrierFactory Factory { get; set; }

        protected override void _Execute()
        {
            Rectangle position = GetBarrierBosition(Character.FacingDirection, Character.ColisionRectangle);
            Character.Map.Add(Factory.CreateBarrier(EBarrierType.Stone, position));
        }

        private Rectangle GetBarrierBosition(Vector2 dir, Rectangle creatorRect)
        {
            int width = 32, height = 32;
            float x = 0, y = 0;
            int cx = creatorRect.X + creatorRect.Width / 2 - width / 2;
            int cy = creatorRect.Y + creatorRect.Height / 2 - height / 2;
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
                    x = creatorRect.X + creatorRect.Width;
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
                    y = creatorRect.Y + creatorRect.Height;
                }
                x = cx + ((cy - y) * dir.X) / dir.Y;
            }
            return new Rectangle((int)x, (int)y, width, height);
        }
    }
}
