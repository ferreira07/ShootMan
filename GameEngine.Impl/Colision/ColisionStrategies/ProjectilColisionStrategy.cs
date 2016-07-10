using GameEngine.Colision;
using GameEngine.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Colision.ColisionStrategies
{
    public class ProjectilColisionStrategy : IColisionStrategy
    {
        public Projectil Projectil { get; set; }
        public IColider Colider { get; set; }

        public ProjectilColisionStrategy(Projectil projectil, IColider colider)
        {
            Projectil = projectil;
            Colider = colider;
        }
        public void ProcessColision()
        {
            Colider.Damage(Projectil.DamageAmmount);
            Projectil.Piercy--;
            if (Projectil.Piercy < 0)
                Projectil.Remove();
        }
    }
}
