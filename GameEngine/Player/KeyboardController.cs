﻿using System;
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
        public Keys Fire { get; set; }
        public Keys Fire2 { get; set; }
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
                Fire = Keys.J;
                Fire2 = Keys.H;
                Pause = Keys.Space;
                Cancel = Keys.K;
            }
            else
            {
                Up = Keys.Up;
                Down = Keys.Down;
                Left = Keys.Left;
                Right = Keys.Right;
                Fire = Keys.NumPad2;
                Fire2 = Keys.NumPad1;
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

        public bool Action(EControllerButton type)
        {
            if (type == EControllerButton.Fire)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Fire) : false;
                return old && state.IsKeyUp(Fire);
            }
            else if (type == EControllerButton.Fire2)
            {
                bool old = oldState != null ? oldState.IsKeyDown(Fire2) : false;
                return old && state.IsKeyUp(Fire2);
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
