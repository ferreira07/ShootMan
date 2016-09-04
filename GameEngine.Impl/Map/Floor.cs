using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using GameEngine.Draw;
using Microsoft.Xna.Framework;

namespace GameEngine.Impl.Map
{
    public class Floor : DrawableObject, IMapObject
    {
        public Floor(Sprite sprite, Rectangle position)
        {
            this.Sprite = sprite;
            this.DrawRectangle = position;
        }
        public IBattleMapFacade Map { get; set; }
    }
}
