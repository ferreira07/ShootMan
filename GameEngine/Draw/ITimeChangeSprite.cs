﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public interface ITimeChangeSprite
    {
        void PassTime(TimeSpan time);
    }
}
