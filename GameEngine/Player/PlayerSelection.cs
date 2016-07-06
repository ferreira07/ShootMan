using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class PlayerSelection
    {
        public static ECharacterType MAX_TYPE = Enum.GetValues(typeof(ECharacterType)).Cast<ECharacterType>().Max();
        public static ECharacterType MIN_TYPE = Enum.GetValues(typeof(ECharacterType)).Cast<ECharacterType>().Min();

        public IController Controller { get; set; }
        public ECharacterType Type { get; set; }
        public bool Confirmed { get; set; }

        public void Update()
        {
            Controller.UpdateState();

            if (Controller.Action(EControllerAction.Release1))
            {
                Confirmed = true;
            }
            if (Controller.Action(EControllerAction.Cancel))
            {
                Confirmed = false;
            }
            if (!Confirmed)
            {
                if (Controller.Action(EControllerAction.LeftPressed))
                {
                    Type--;
                    if(Type < 0) Type = MAX_TYPE; 
                }
                if (Controller.Action(EControllerAction.RightPressed))
                {
                    Type++;
                    if (Type > MAX_TYPE) Type = MIN_TYPE;
                }
            }
        }
    }
}
