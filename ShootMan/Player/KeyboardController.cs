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
        
        public KeyboardController()
        {
            Up = Keys.Up;
            Down = Keys.Down;
            Left = Keys.Left;
            Right = Keys.Right;
        }
        public Vector2 Direction()
        {
            int x = 0;
            int y = 0;
            KeyboardState state = Keyboard.GetState();
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
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyDown(Keys.Space);
        }
    }
}
