﻿using GameEngine.Colision;
using GameEngine.Map.Obstacle;
using GameEngine.Player;
using GameEngine.Util;
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
            int width = 32, height = 32;
            RectangleF position = new RectangleF(CreateObjectHelper.GetStartPosition(Character.FacingDirection, Character.ColisionRectangle, width, height), width, height);

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
                    x = creatorRect.X - width - 1;
                }
                else
                {
                    x = creatorRect.X + creatorRect.Width + 1;
                }
                y = cy + ((cx - x) * dir.Y) / dir.X;
            }
            else
            {
                //Borda Lateral
                if (dir.Y > 0)
                {
                    y = creatorRect.Y - height -1;
                }
                else
                {
                    y = creatorRect.Y + creatorRect.Height +1;
                }
                x = cx + ((cy - y) * dir.X) / dir.Y;
            }
            return new RectangleF(x, y, width, height);
        }
    }
}
