using Microsoft.Xna.Framework;
using GameEngine.Draw;
using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;

namespace GameEngine.Impl.Colision
{
    public class Barrier : MapObject
    {
        private int MaxHp { get; set; }
        public int Hp { get; set; }

        public Barrier(Rectangle position, int hp, Sprite sprite)
        {
            Sprite = sprite;
            DrawRectangle = position;
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
            SetHp(hp);
            SelectSprite();
        }

        internal void SetHp(int value)
        {
            MaxHp = value;
            Hp = value;
        }

        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground | EColisionLayer.Floating;
            }
        }

        public override EColisionType ColisionType { get { return EColisionType.Blocking; } }

        public override void Damage(int ammount)
        {
            this.Hp -= ammount;
            SelectSprite();
            if (Hp <= 0)
            {
                Hp = 0;
                OnZeroHp();
            }
        }

        protected virtual void OnZeroHp()
        {
            //TODO comportamento adequado para barreira destruída
            Map.Remove(this);
        }

        protected void SelectSprite()
        {
            double damagePercent = 1.0*(MaxHp - Hp) / MaxHp;
            if ( this.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Damage))
            {
                (this.Sprite as IDamageChangeSprite).SetDamagePercent(damagePercent);
            }
        }
    }
}
