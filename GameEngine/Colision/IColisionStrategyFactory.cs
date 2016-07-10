using GameEngine.Colision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Colision
{
    public interface IColisionStrategyFactory
    {
        IColisionStrategy Create(IColider item1, IColider item2);
    }
}
