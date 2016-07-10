using GameEngine.Colision;
using GameEngine.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Impl.Colision.ColisionStrategies
{
    public class PowerUpCharacterColisionStrategy : IColisionStrategy
    {
        public IPowerUp PowerUp { get; set; }
        public Character Character { get; set; }

        public PowerUpCharacterColisionStrategy(IPowerUp powerUp, Character character)
        {
            PowerUp = powerUp;
            Character = character;
        }
        public void ProcessColision()
        {
            PowerUp.Aplly(Character);
        }
    }
}
