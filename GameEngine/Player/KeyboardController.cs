using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Player
{
    public class KeyboardController : IController
    {
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Action1 { get; set; }
        public Keys Action2 { get; set; }
        public Keys Action3 { get; set; }
        public Keys Action4 { get; set; }
        public Keys Pause { get; set; }
        public Keys Cancel { get; set; }

        public int KeyboardIndex { get; private set; }

        public KeyboardController(int controller)
        {
            KeyboardIndex = controller;
            if (controller == 0)
            {
                Up = Keys.W;
                Down = Keys.S;
                Left = Keys.A;
                Right = Keys.D;
                Action1 = Keys.J;
                Action2 = Keys.K;
                Action3 = Keys.H;
                Action4 = Keys.U;
                Pause = Keys.Space;
                Cancel = Keys.K;
            }
            else
            {
                Up = Keys.Up;
                Down = Keys.Down;
                Left = Keys.Left;
                Right = Keys.Right;
                Action1 = Keys.NumPad2;
                Action2 = Keys.NumPad3;
                Action3 = Keys.NumPad1;
                Action4 = Keys.NumPad5;
                Pause = Keys.Enter;
                Cancel = Keys.NumPad3;
            }
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

        public bool Action(EControllerAction type)
        {
            if (type == EControllerAction.Release1)
            {
                return Release(Action1);
            }
            else if (type == EControllerAction.StartCharge1)
            {
                return StartCharge(Action1);
            }
            else if (type == EControllerAction.Release2)
            {
                return Release(Action2);
            }
            else if (type == EControllerAction.StartCharge2)
            {
                return StartCharge(Action2);
            }
            else if (type == EControllerAction.Release3)
            {
                return Release(Action3);
            }
            else if (type == EControllerAction.StartCharge3)
            {
                return StartCharge(Action3);
            }
            else if (type == EControllerAction.Release4)
            {
                return Release(Action4);
            }
            else if (type == EControllerAction.StartCharge4)
            {
                return StartCharge(Action4);
            }
            else if (type == EControllerAction.LeftPressed)
            {
                return Release(Left);
            }
            else if (type == EControllerAction.RightPressed)
            {
                return Release(Right);
            }
            else if (type == EControllerAction.Pause)
            {
                return Release(Pause);
            }
            else if (type == EControllerAction.Cancel)
            {
                return Release(Cancel);
            }
            return false;
        }

        private bool StartCharge(Keys key)
        {
            bool old = oldState != null ? oldState.IsKeyUp(key) : false;
            return old && state.IsKeyDown(key);
        }

        private bool Release(Keys key)
        {
            bool old = oldState != null ? oldState.IsKeyDown(key) : false;
            return old && state.IsKeyUp(key);
        }
    }
}
