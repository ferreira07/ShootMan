using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public abstract class Action
    {
        public int MpCost { get; set; }
        // TODO Realizar disparos com atraso
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
                Character.ExpendMp(MpCost);
                Character.Fatigated(ExaustingTime);
                Execute();
            };
        }
        public void Execute()
        {
            _Execute();
        }

        protected abstract void _Execute();
    }
}
