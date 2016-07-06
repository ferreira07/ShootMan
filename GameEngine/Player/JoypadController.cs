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

        public bool Action(EControllerAction type)
        {
            if (type == EControllerAction.Release1)
            {
                return Release(Buttons.A);
            }
            else if (type == EControllerAction.StartCharge1)
            {
                return StartCharge(Buttons.A);
            }
            else if (type == EControllerAction.Release2)
            {
                return Release(Buttons.B);
            }
            else if (type == EControllerAction.StartCharge2)
            {
                return StartCharge(Buttons.B);
            }
            else if (type == EControllerAction.Release3)
            {
                return Release(Buttons.X);
            }
            else if (type == EControllerAction.StartCharge3)
            {
                return StartCharge(Buttons.X);
            }
            else if (type == EControllerAction.Release4)
            {
                return Release(Buttons.Y);
            }
            else if (type == EControllerAction.StartCharge4)
            {
                return StartCharge(Buttons.Y);
            }
            else if (type == EControllerAction.LeftPressed)
            {
                return Release(Buttons.DPadLeft);
            }
            else if (type == EControllerAction.RightPressed)
            {
                return Release(Buttons.DPadRight);
            }
            else if (type == EControllerAction.Pause)
            {
                return Release(Buttons.Start);
            }
            else if (type == EControllerAction.Cancel)
            {
                return Release(Buttons.B);
            }
            return false;
        }

        private bool Release(Buttons button)
        {
            bool old = oldState != null ? oldState.IsButtonDown(button) : false;
            return old && state.IsButtonUp(button);
        }
        private bool StartCharge(Buttons button)
        {
            bool old = oldState != null ? oldState.IsButtonUp(button) : false;
            return old && state.IsButtonDown(button);
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
