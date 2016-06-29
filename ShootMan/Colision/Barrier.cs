using Microsoft.Xna.Framework;
using ShootMan.Draw;
using ShootMan.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Colision
{
    public class Barrier : MapObject
    {

        private int MaxHp { get; set; }
        public int Hp { get; set; }
        private Sprite[] Sprites;

        public Barrier(Rectangle position, int hp, params Sprite[] sprites)
        {
            Sprites = sprites;
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
                //TODO comportamento adequado para barreira destruída
                Map.Remove(this);
            }
        }

        private void SelectSprite()
        {
            double percent = 1.0*(MaxHp - Hp) / MaxHp;
            int pos = (int)Math.Min(Math.Floor(percent * this.Sprites.Count()), this.Sprites.Count() - 1);
            this.Sprite = Sprites[pos];
        }
    }
}
