using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Player
{
    public interface IController
    {
        void UpdateState();
        Vector2 Direction();
        bool Action(EControllerButton type);
    }
}
