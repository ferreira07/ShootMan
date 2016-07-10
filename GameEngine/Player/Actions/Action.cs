using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public abstract class Action
    {
        public Action() { }
        public Action(EControllerAction action, int mpCost, TimeSpan exaustingActionTime, TimeSpan exaustingMoveTime, TimeSpan chargingTime)
        {
            ControllerAction = action;
            MpCost = mpCost;
            ExaustingActionTime = exaustingActionTime;
            ExaustingMoveTime = exaustingMoveTime;
            ChargingTime = chargingTime;
        }
        public int MpCost { get; set; }
        public TimeSpan ChargingTime { get; set; }

        /// <summary>
        /// Tempo de Fadiga para o personagem
        /// </summary>
        public TimeSpan ExaustingActionTime { get; set; }
        public TimeSpan ExaustingMoveTime { get; set; }
        public Character Character { get; set; }
        public EControllerAction ControllerAction { get; set; }

        public void Check()
        {
            if (Character.Controller.Action(ControllerAction) &&
                Character.Mp >= MpCost)
            {

                Character.ExpendMp(MpCost);
                Character.ChargingAction = new ChargingAction() { Action = this, RemainTime = ChargingTime };
            };
        }

        public void StartCharge()
        {
            _Execute();
        }

        public void Execute()
        {
            Character.Fatigated(ExaustingActionTime, ExaustingMoveTime);
            _Execute();
        }

        protected abstract void _Execute();
    }
}
