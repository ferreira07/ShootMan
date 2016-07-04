using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Player
{
    public class JoypadController : IController
    {

        public JoypadController(int gamePadIndex)
        {
            GamePadIndex = gamePadIndex;
        }

        public int GamePadIndex { get; private set; }

        public EControllerType ControllerType
        {
            get { return EControllerType.Joypad; }
        }

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
            }
            else if (type == EControllerButton.LeftPressed)
            {
                bool old = oldState != null ? oldState.IsButtonDown(Buttons.DPadLeft) : false;
                return old && state.IsButtonUp(Buttons.DPadLeft);
            }
            else if (type == EControllerButton.RightPressed)
            {
                bool old = oldState != null ? oldState.IsButtonDown(Buttons.DPadRight) : false;
                return old && state.IsButtonUp(Buttons.DPadRight);
            }
            else if (type == EControllerButton.Pause)
            {
                bool old = oldState != null ? oldState.IsButtonDown(Buttons.Start) : false;
                return old && state.IsButtonUp(Buttons.Start);
            }
            else if (type == EControllerButton.Cancel)
            {
                bool old = oldState != null ? oldState.IsButtonDown(Buttons.B) : false;
                return old && state.IsButtonUp(Buttons.B);
            }
            else if (type == EControllerButton.StartCharge)
            {
                bool old = oldState != null ? oldState.IsButtonUp(Buttons.A) : false;
                return old && state.IsButtonDown(Buttons.A);
            }
            return false;
        }

        public Vector2 Direction()
        {
            Vector2 v = state.ThumbSticks.Left;
            if (v == Vector2.Zero)
            {
                int x = 0;
                int y = 0;
                if (state.IsButtonDown(Buttons.DPadUp)) y += 1;
                if (state.IsButtonDown(Buttons.DPadDown)) y -= 1;
                if (state.IsButtonDown(Buttons.DPadLeft)) x -= 1;
                if (state.IsButtonDown(Buttons.DPadRight)) x += 1;

                v = new Vector2(x, y);
                if (v != Vector2.Zero) v.Normalize();
            }
            return v;
        }
    }
}
