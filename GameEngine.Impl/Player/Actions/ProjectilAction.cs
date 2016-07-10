using GameEngine.Move;
using GameEngine.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Player.Actions
{
    public class ProjectilAction : GameEngine.Player.Action
    {
        public ProjectilAction()
        {

        }
        public ProjectilAction(EControllerAction action, int mpCost, EProjectilType projectilTipe, TimeSpan exaustingActionTime)
            : this(action, mpCost, projectilTipe, exaustingActionTime, TimeSpan.Zero, TimeSpan.Zero)
        { }

        public ProjectilAction(EControllerAction action, int mpCost, EProjectilType projectilTipe, TimeSpan exaustingActionTime, TimeSpan exaustingMoveTime, TimeSpan chargingTime)
            : base(action, mpCost, exaustingActionTime, exaustingMoveTime, chargingTime)
        {
            ProjectilType = projectilTipe;
        }

        public EProjectilType ProjectilType { get; set; }

        protected override void _Execute()
        {
            Character.Map.Add(ProjectilFactory.Create(ProjectilType, Character.FacingDirection, Character.ColisionRectangle));
        }
    }
}
