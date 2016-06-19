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

        public KeyboardController()
        {
            Up = Keys.W;
            Down = Keys.S;
            Left = Keys.A;
            Right = Keys.D;
            Fire = Keys.Down;
        }

        private KeyboardState oldState;
        private KeyboardState state;
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
            else if (type == EControllerButton.StartCharge)
            {
                bool old = oldState != null ? oldState.IsKeyUp(Fire) : false;
                return old && state.IsKeyDown(Fire);
            }
            return false;
        }
    }
}
