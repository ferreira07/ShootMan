using GameEngine.Colision;
using GameEngine.Map;
using Microsoft.Xna.Framework;

namespace GameEngine.Map.Obstacle
{
    public interface IBarrierFactory
    {
        IMapObject CreateBarrier(EBarrierType type, RectangleF position);
    }
}