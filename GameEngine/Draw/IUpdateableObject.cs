using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public interface IUpdateableObject
    {
        void Update(TimeSpan elapsedGameTime);
    }
}
