using GameEngine.Colision;
using GameEngine.Draw;
using GameEngine.Move;
using GameEngine.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Map
{
    public partial class BattleMap
    {


        public BattleMap()
        {
            ColisionObjects = new List<IColider>();
            MapObjects = new List<IMapObject>();
            Characters = new List<Character>();
            DrawableObjects = new List<DrawableObject>();
            _ToRemove = new List<IMapObject>();
        }


        #region Objects Manage 
        public List<IColider> ColisionObjects { get; private set; }
        public List<DrawableObject> DrawableObjects { get; private set; }
        public List<IMapObject> MapObjects { get; private set; }
        public List<Character> Characters { get; private set; }

        private List<IMapObject> _ToRemove;

        public void Add(IMapObject obj)
        {
            MapObjects.Add(obj);

            IColider colider = obj as IColider;
            if (colider != null)
            {
                ColisionObjects.Add(colider);
            }
            Character character = obj as Character;
            if (character != null)
            {
                Characters.Add(character);
            }
            DrawableObject drawable = obj as DrawableObject;
            if (drawable != null)
            {
                DrawableObjects.Add(drawable);
            }
        }
        public void Remove(IMapObject obj)
        {
            _ToRemove.Add(obj);
        }
        public void RemoveObjects()
        {
            foreach (var obj in _ToRemove)
            {
                MapObjects.Remove(obj);
                ColisionObjects.Remove(obj as IColider);
                DrawableObjects.Remove(obj as DrawableObject);
                //Não remover da lista de personagens
            }
            _ToRemove.Clear();
        }

        #endregion

        #region Time Manage

        public TimeSpan RemainTime { get; private set; }

        public void SetTime(TimeSpan time)
        {
            RemainTime = time;
        }
        public void PassTime(TimeSpan elapsedGameTime)
        {
            RemainTime -= elapsedGameTime;
            if (RemainTime <= TimeSpan.Zero) RemainTime = TimeSpan.Zero;
        }
        
        #endregion
    }
}
