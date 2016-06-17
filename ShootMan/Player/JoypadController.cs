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

        public bool Action(EControllerButton type)
        {
            return GamePad.GetState(GamePadIndex).IsButtonDown(Buttons.A);
        }

        public Vector2 Direction()
        {
            return GamePad.GetState(GamePadIndex).ThumbSticks.Left;
        }
    }
}
