using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Colision
{
    public interface IHittable
    {
        void Hit(IColider other);
    }
}
