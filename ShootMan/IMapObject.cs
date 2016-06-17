using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan
{
    public interface IMapObject
    {
        int Id { get; }
        Map Map { get; set; }
    }
}
