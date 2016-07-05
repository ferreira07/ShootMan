using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public interface IFacingChangeSprite
    {
        void SetFacing(Vector2 direction);
    }
}
