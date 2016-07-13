using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Map
{
    public interface IMapObject
    {
        int Id { get; }
        IBattleMapFacade Map { get; set; }
    }
}
