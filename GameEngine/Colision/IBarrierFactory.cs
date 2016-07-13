using GameEngine.Map;
using Microsoft.Xna.Framework;

namespace GameEngine.Colision
{
    public interface IBarrierFactory
    {
        IMapObject CreateBarrier(EBarrierType type, RectangleF position);
    }
}