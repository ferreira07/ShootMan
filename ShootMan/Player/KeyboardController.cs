using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShootMan.Player
{
    public class KeyboardController : IController
    {
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Fire { get; set; }
        public Keys Pause { get; set; }
        public Keys Cancel { get; set; }

        public KeyboardController()
        {
            Up = Keys.W;
            Down = Keys.S;
            Left = Keys.A;
            Right = Keys.D;
            Fire = Keys.Down;
            Pause = Keys.Enter;
            Cancel = Keys.Escape;
        }

        private KeyboardState oldState;
        private KeyboardState state;


        public EControllerType ControllerType
        {
            get { return EControllerType.Keyboard; }
        }

        public void UpdateState()
        {
            oldState = state;
            state = Keyboard.GetState();
        }
        public Vector2 Direction()
        {
            int x = 0;
            int y = 0;
            if (state.IsKeyDown(Up)) y += 1;
            if (state.IsKeyDown(Down)) y -= 1;
            if (state.IsKeyDown(Left)) x -= 1;
            if (state.IsKeyDown(Right)) x += 1;

            Vector2 v = new Vector2(x, y);
            if (v != Vector2.Zero) v.Normalize();
            return v;
        }

        public bool Action(EControllerButton type)
        {
            if (type == EControllerButton.Fire)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Fire) : false;
                return old && state.IsKeyUp(Fire);
            }
            else if (type == EControllerButton.LeftPressed)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Left) : false;
                return old && state.IsKeyUp(Left);
            }
            else if (type == EControllerButton.RightPressed)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Right) : false;
                return old && state.IsKeyUp(Right);
            }
            else if (type == EControllerButton.Pause)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Pause) : false;
                return old && state.IsKeyUp(Pause);
            }
            else if (type == EControllerButton.Cancel)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Cancel) : false;
                return old && state.IsKeyUp(Cancel);
            }
            else if (type == EControllerButton.StartCharge)
            {
                bool old = oldState != null ? oldState.IsKeyUp(Fire) : false;
                return old && state.IsKeyDown(Fire);
            }
            return false;
        }
    }
}
