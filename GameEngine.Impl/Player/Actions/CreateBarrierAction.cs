using GameEngine.Colision;
using GameEngine.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Player.Actions
{
    public class CreateBarrierAction : GameEngine.Player.Action
    {
        public CreateBarrierAction(EControllerAction action, int mpCost, IBarrierFactory factory, TimeSpan exaustingActionTime, TimeSpan exaustingMoveTime, TimeSpan chargingTime)
            : base(action, mpCost, exaustingActionTime, exaustingMoveTime, chargingTime)
        {
            Factory = factory;
        }

        public IBarrierFactory Factory { get; set; }

        protected override void _Execute()
        {
            RectangleF position = GetBarrierPosition(Character.FacingDirection, Character.ColisionRectangle);
            Character.Map.Add(Factory.CreateBarrier(EBarrierType.Stone, position));
        }

        private RectangleF GetBarrierPosition(Vector2 dir, RectangleF creatorRect)
        {
            int width = 32, height = 32;
            float x = 0, y = 0;
            float cx = creatorRect.X + creatorRect.Width / 2 - width / 2;
            float cy = creatorRect.Y + creatorRect.Height / 2 - height / 2;
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
            return new RectangleF(x, y, width, height);
        }
    }
}
