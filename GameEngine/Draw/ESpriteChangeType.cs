using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    [Flags]
    public enum ESpriteChangeType
    {
        None = 0x1,
        Damage = 0x2,
        Time = 0x4,
        Move = 0x8,
        Facing = 0x10
    }
}
