using GameEngine.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class ProjectilAction : Action
    {
        public ProjectilAction()
        {

        }
        public ProjectilAction(EControllerAction action, int mpCost, EProjectilType projectilTipe, TimeSpan exaustingTime)
        {
            ControllerAction = action;
            MpCost = mpCost;
            ProjectilType = projectilTipe;
            ExaustingTime = exaustingTime;
        }
        public EProjectilType ProjectilType { get; set; }

        protected override void _Execute()
        {
            Character.Map.Add(ProjectilFactory.Create(ProjectilType, Character.FacingDirection, Character.ColisionRectangle));
        }
    }
}
