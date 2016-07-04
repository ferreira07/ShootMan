using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public interface IController
    {
        EControllerType ControllerType { get; }
        void UpdateState();
        Vector2 Direction();
        bool Action(EControllerButton type);
    }
}
