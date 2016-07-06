﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Colision;
using GameEngine.Draw;
using GameEngine.Map;
using GameEngine.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player
{
    public class Character : MovingObject
    {
        public override EColisionType ColisionType
        {
            get { return EColisionType.Blocking; }
        }

        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }

        public bool IsDead { get { return Hp <= 0; } }
        
        public IController Controller { get; set; }
        public TimeSpan LastShoot { get; private set; }

        internal void SetHp(int value)
        {
            Hp = value;
            MaxHp = value;
        }
        internal void SetMp(int value)
        {
            Mp = value;
            MaxMp = value;
        }

        public TimeSpan ShootTime { get; private set; }
        public bool IsCharged(TimeSpan time)
        {
            return StartChargeShoot != TimeSpan.Zero && StartChargeShoot + TimeSpan.FromSeconds(1)< time;
        }
        private TimeSpan StartChargeShoot { get; set; }


        public override EColisionLayer ColisionLayer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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


            if (v != Vector2.Zero)
            {
                FacingDirection = v;
                if (this.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Facing))
                {
                    (Sprite as IFacingChangeSprite).SetFacing(FacingDirection);
                }
            }
            if (Controller.Action(EControllerButton.Fire) && CanShoot(gameTime.TotalGameTime))
            {
                //criar um novo projétil
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
            if (Controller.Action(EControllerButton.Fire2) && CanShoot(gameTime.TotalGameTime))
            {
                //criar um novo projétil
                Map.Add(ProjectilFactory.Create(EProjectilType.Fireball, FacingDirection, this.ColisionRectangle));
                _CanShoot = false;
                this.LastShoot = gameTime.TotalGameTime;
                StartChargeShoot = TimeSpan.Zero;
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