using ShootMan.Colision;
using ShootMan.Draw;
using ShootMan.Move;
using ShootMan.Player;
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
        public List<Character> Characters { get; private set; }

        public Map()
        {
            ColisionObjects = new List<IColider>();
            MapObjects = new List<IMapObject>();
            Characters = new List<Character>();
        }

        public void Add(IMapObject obj)
        {
            MapObjects.Add(obj);

            IColider colider = obj as IColider;
            if(colider!= null)
            {
                ColisionObjects.Add(colider);
            }
            Character character = obj as Character;
            if (character != null)
            {
                Characters.Add(character);
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

            //Não remover da lista de personagens
        }
    }
}
