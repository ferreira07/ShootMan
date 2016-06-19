using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShootMan.Colision;
using ShootMan.Draw;
using ShootMan.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Player
{
    public class Character : MovingObject
    {
        public override EColisionType ColisionType
        {
            get { return EColisionType.Blocking; }
        }

        public int Hp { get; set; }

        public bool IsDead { get { return Hp <= 0; } }
        
        public IController Controller { get; set; }
        public TimeSpan LastShoot { get; private set; }
        public TimeSpan ShootTime { get; private set; }
        public bool IsCharged(TimeSpan time)
        {
            return StartChargeShoot != TimeSpan.Zero && StartChargeShoot + TimeSpan.FromSeconds(1)< time;
        }
        private TimeSpan StartChargeShoot { get; set; }
        private Vector2 FacingDirection;

        private bool _CanShoot;

        public Character(Sprite sprite, IController controller)
        {            
            Sprite = sprite;
            this.DrawRectangle = Sprite.SourceRectangle;
            UpdateRectangle();
            Controller = controller;
            ShootTime = TimeSpan.FromMilliseconds(200);
        }       

        public override void Update(GameTime gameTime)
        {
            Controller.UpdateState();

            Vector2 v = Controller.Direction();

            if (v != Vector2.Zero) FacingDirection = v;

            if (Controller.Action(EControllerButton.Fire) && CanShoot(gameTime.TotalGameTime))
            {
                //criar um novo projétil
                Vector2 position = Position + (FacingDirection * 30 * new Vector2(1, -1));
                EProjectilType projectilType = IsCharged(gameTime.TotalGameTime)? EProjectilType.ChargedBullet: EProjectilType.Bullet;
                Map.Add(ProjectilFactory.Create(projectilType, FacingDirection, this.ColisionRectangle));
                _CanShoot = false;
                this.LastShoot = gameTime.TotalGameTime;
                StartChargeShoot = TimeSpan.Zero;
            }
            else if (Controller.Action(EControllerButton.StartCharge))
            {
                //TODO CanCharge
                this.StartChargeShoot = gameTime.TotalGameTime;
            }
            Speed = v * MaxSpeed;
            base.Update(gameTime);
        }
        
        private bool CanShoot(TimeSpan timeSpan)
        {
            if (!_CanShoot)
            {
                if (timeSpan - LastShoot > ShootTime)
                {
                    _CanShoot = true;
                }
            }
            return _CanShoot;
        }

        public override void Damage(int ammount)
        {
            this.Hp -= ammount;
            if (Hp <= 0)
            {
                Hp = 0;
                //TODO comportamento adequado para personagem morto
                Map.Remove(this);
            }
        }
    }
}
