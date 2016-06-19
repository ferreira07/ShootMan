using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShootMan.Player
{
    public class JoypadController : IController
    {

        public JoypadController(int gamePadIndex)
        {
            GamePadIndex = gamePadIndex;
        }

        public int GamePadIndex { get; private set; }

        private GamePadState oldState;
        private GamePadState state;
        public void UpdateState()
        {
            oldState = state;
            state = GamePad.GetState(GamePadIndex);
        }

        public bool Action(EControllerButton type)
        {
            if (type == EControllerButton.Fire)
            {
                bool old = oldState != null ? oldState.IsButtonDown(Buttons.A) : false;
                return old && state.IsButtonUp(Buttons.A);
            }else if (type == EControllerButton.StartCharge)
            {
                bool old = oldState != null ? oldState.IsButtonUp(Buttons.A) : false;
                return old && state.IsButtonDown(Buttons.A);
            }
            return false;
        }

        public Vector2 Direction()
        {
            return state.ThumbSticks.Left;
            //TODO verificar os direcionais
        }
    }
}
