using GameEngine.Colision;
using GameEngine.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Combat;

namespace GameEngine.Map
{
    public abstract class MapObject : DrawableObject, IColider, IMapObject, IDefensesContainer
    {
        public abstract EColisionLayer ColisionLayer { get; }
        public RectangleF ColisionRectangle { get; set; }
        public abstract EColisionType ColisionType { get; }

        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public void SetHp(int value)
        {
            Hp = value;
            MaxHp = value;
        }
        public virtual void Damage(int ammount)
        {
            this.Hp -= ammount;
            if (this.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Damage))
            {
                double damagePercent = 1.0 * (MaxHp - Hp) / MaxHp;
                (this.Sprite as IDamageChangeSprite).SetDamagePercent(damagePercent);
            }
            if (Hp <= 0)
            {
                Hp = 0;
                OnZeroHp();
            }
        }

        public virtual void AddStatus(Status status)
        {
            
        }
        protected virtual void OnZeroHp()
        {
            //TODO comportamento adequado para objeto destruido
            Remove();
        }
        
        public IBattleMapFacade Map { get; set; }

        public void Remove()
        {
            Map.Remove(this);
        }

        public Defenses Defenses { get; set; }

        public Defenses GetDefenses()
        {
            return Defenses;
        }
    }
}
