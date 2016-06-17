using ShootMan.Colision;
using ShootMan.Draw;
using ShootMan.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan
{
    public class Map
    {
        public List<IColider> ColisionObjects { get; private set; }
        public List<IMapObject> MapObjects { get; private set; }

        public Map()
        {
            ColisionObjects = new List<IColider>();
            MapObjects = new List<IMapObject>();
        }

        public void Add(IMapObject obj)
        {
            MapObjects.Add(obj);

            IColider colider = obj as IColider;
            if(colider!= null)
            {
                ColisionObjects.Add(colider);
            }

            MovingObject movingObject = obj as MovingObject;
            if(movingObject!= null)
            {
                movingObject.Map = this;
            }
        }

        internal void Remove(IMapObject obj)
        {
            MapObjects.Remove(obj);
            ColisionObjects.Remove(obj as IColider);
        }
    }
}
