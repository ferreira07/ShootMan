using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class ChargingAction
    {
        public TimeSpan RemainTime { get; set; }
        public Action Action { get; set; }
    }
}
