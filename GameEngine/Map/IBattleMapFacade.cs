using System;
using System.Collections.Generic;
using GameEngine.Colision;
using GameEngine.Draw;
using GameEngine.Move;
using GameEngine.Player;

namespace GameEngine.Map
{
    public interface IBattleMapFacade
    {
        List<Character> Characters { get; }
        List<IColider> ColisionObjects { get; }
        List<DrawableObject> DrawableObjects { get; }
        TimeSpan RemainTime { get; }

        void Add(IMapObject mapObject);
        bool IsGameOver();
        void Move(MovingObject movingObject, TimeSpan elapsedGameTime);
        void Remove(IMapObject mapObject);
        void SetTime(TimeSpan timeSpan);
        void StartNewFrame(TimeSpan elapsedGameTime);
        void DoDamage(IAttackContainer attackContainer, IDefensesContainer defense);
        void VerifyColision();
    }
}