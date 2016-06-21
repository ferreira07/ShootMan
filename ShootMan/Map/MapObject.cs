using ShootMan.Colision;
using ShootMan.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootMan.Map
{
    public abstract class MapObject : DrawableObject, IColider, IMapObject
    {
        public abstract EColisionLayer ColisionLayer { get; }
        public Rectangle ColisionRectangle { get; set; }
        public abstract EColisionType ColisionType { get; }
        
        public abstract void Damage(int ammount);

        public BattleMap Map { get; set; }
    }
}
