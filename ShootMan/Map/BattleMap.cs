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
    public class BattleMap
    {
        public TimeSpan RemainTime { get; private set; }
        public List<IColider> ColisionObjects { get; private set; }
        public List<IMapObject> MapObjects { get; private set; }
        public List<Character> Characters { get; private set; }

        public BattleMap()
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
            obj.Map = this;
        }
        public void SetTime(TimeSpan time)
        {
            RemainTime = time;
        }
        internal void Remove(IMapObject obj)
        {
            MapObjects.Remove(obj);
            ColisionObjects.Remove(obj as IColider);

            //Não remover da lista de personagens
        }

        internal void PassTime(TimeSpan elapsedGameTime)
        {
            RemainTime -= elapsedGameTime;
            if (RemainTime <= TimeSpan.Zero) RemainTime = TimeSpan.Zero;
        }
    }
}
