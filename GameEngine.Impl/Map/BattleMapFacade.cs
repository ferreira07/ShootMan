using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Move;
using GameEngine.Player;
using GameEngine.Colision;
using GameEngine.Draw;
using GameEngine.Impl.Colision;

namespace GameEngine.Map
{
    public class BattleMapFacade : IBattleMapFacade
    {
        public BattleMapFacade()
        {
            this.Map = new BattleMap();
            ColisionManager = new ColisionManager();
            ColisionManager.ColisionStrategyFactory = new ColisionStrategyFactory();
            DamageManager = new DamageManager();
        }
        private BattleMap Map { get; set; }
        private IColisionManager ColisionManager { get; set; }
        private IDamageManager DamageManager { get; set; }

        public List<Character> Characters { get { return Map.Characters; } }
        public List<IColider> ColisionObjects { get { return Map.ColisionObjects; } }
        public List<DrawableObject> DrawableObjects { get { return Map.DrawableObjects; } }

        public TimeSpan RemainTime { get { return Map.RemainTime; } }

        public void StartNewFrame(TimeSpan elapsedGameTime)
        {
            Map.PassTime(elapsedGameTime);
            Map.RemoveObjects();
        }

        public void Move(MovingObject movingObject, TimeSpan elapsedGameTime)
        {
            ColisionManager.Move(movingObject, elapsedGameTime, Map.ColisionObjects);
        }

        public void Remove(IMapObject mapObject)
        {
            Map.Remove(mapObject);
        }

        public void Add(IMapObject mapObject)
        {
            Map.Add(mapObject);
            mapObject.Map = this;
        }

        public bool IsGameOver()
        {
            return !(Map.Characters.Where(c => c.Hp > 0).Count() > 1) || Map.RemainTime == TimeSpan.Zero;
        }
        
        public void VerifyColision()
        {
            ColisionManager.VerifyColision(Map.ColisionObjects);
        }

        public void SetTime(TimeSpan timeSpan)
        {
            Map.SetTime(timeSpan);
        }

        public void DoDamage(IAttackContainer attackContainer, IDefensesContainer defenseContainer)
        {
            DamageManager.DoDamage(attackContainer, defenseContainer);
        }
    }
}
