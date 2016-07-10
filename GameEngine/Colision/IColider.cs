using Microsoft.Xna.Framework;

namespace GameEngine.Colision
{
    public interface IColider
    {
        Rectangle ColisionRectangle { get; }

        EColisionLayer ColisionLayer { get; }

        EColisionType ColisionType { get; }

        void Damage(int ammount);        
    }
}