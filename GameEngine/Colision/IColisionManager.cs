using System;
using System.Collections.Generic;
using GameEngine.Move;

namespace GameEngine.Colision
{
    public interface IColisionManager
    {
        IColisionStrategyFactory ColisionStrategyFactory { get; set; }

        void Move(MovingObject obj, TimeSpan elapsedGameTime, List<IColider> ColisionObjects);
        void VerifyColision(List<IColider> ColisionObjects);
    }
}