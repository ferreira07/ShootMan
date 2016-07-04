using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public abstract class SpriteList : Sprite
    {
        public Rectangle[] Positions { get; set; }
    }
}
