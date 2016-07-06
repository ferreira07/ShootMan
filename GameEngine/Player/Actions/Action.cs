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
        public Action(EControllerAction action, int mpCost, TimeSpan exaustingTime, TimeSpan chargingTime)
        {
            ControllerAction = action;
            MpCost = mpCost;
            ExaustingTime = exaustingTime;
            ChargingTime = chargingTime;
        }
        public int MpCost { get; set; }
        // TODO Realizar ações com atraso
        public TimeSpan ChargingTime { get; set; }

        /// <summary>
        /// Tempo de Fadiga para o personagem
        /// </summary>
        public TimeSpan ExaustingTime { get; set; }
        public Character Character { get; set; }
        public EControllerAction ControllerAction { get; set; }

        public void Check()
        {
            if (!Character.IsFatigated() &&
                Character.Controller.Action(ControllerAction) &&
                Character.Mp >= MpCost)
            {
                if (this.ChargingTime == TimeSpan.Zero)
                {
                    Character.ExpendMp(MpCost);
                    Character.Fatigated(ExaustingTime);
                    Execute();
                }
                else
                {
                    Character.ChargingAction = new ChargingAction() { Action = this, RemainTime = ChargingTime };
                }
            };
        }

        public void StartCharge()
        {
            _Execute();
        }

        public void Execute()
        {
            _Execute();
        }

        protected abstract void _Execute();
    }
}
