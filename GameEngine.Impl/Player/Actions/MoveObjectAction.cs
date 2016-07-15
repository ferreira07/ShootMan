using GameEngine.Colision;
using GameEngine.Impl.Colision;
using GameEngine.Impl.Map.Obstacle;
using GameEngine.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Player.Actions
{
    public class MoveObjectAction : GameEngine.Player.Action
    {
        public MoveObjectAction()
        {

        }
        public MoveObjectAction(EControllerAction action, int mpCost, TimeSpan exaustingActionTime)
            : this(action, mpCost, exaustingActionTime, TimeSpan.Zero, TimeSpan.Zero)
        { }

        public MoveObjectAction(EControllerAction action, int mpCost, TimeSpan exaustingActionTime, TimeSpan exaustingMoveTime, TimeSpan chargingTime)
            : base(action, mpCost, exaustingActionTime, exaustingMoveTime, chargingTime)
        {
        }

        private int Tolerance = 20;

        protected override void _Execute()
        {
            RectangleF position = GetAreaPosition(Character.FacingDirection, Character.ColisionRectangle);
            foreach (var item in Character.Map.ColisionObjects.Where(c =>
                c is MovableBarrier &&
                c.ColisionType == EColisionType.Blocking))
            {
                if (item.ColisionRectangle.Intersects(position))
                {
                    (item as MovableBarrier).StartMove(Character.FacingDirection);
                }                
            }
        }

        private RectangleF GetAreaPosition(Vector2 facingDirection, RectangleF colisionRectangle)
        {
            float px = colisionRectangle.X + facingDirection.X * (colisionRectangle.Width / 2 + Tolerance);
            float py = colisionRectangle.Y - facingDirection.Y * (colisionRectangle.Height / 2 + Tolerance);
            return new RectangleF(px, py, colisionRectangle.Width, colisionRectangle.Height);
        }
    }
}
