using Microsoft.Xna.Framework;
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
        public int MaxHp { get; set; }

        public int Mp { get { return (int)Math.Floor(RealMp); } }
        public float RealMp { get; set; }
        public float MpRechargeSpeed { get; set; }
        public int MaxMp { get; set; }

        public List<Action> Actions { get; private set; }
        public bool IsDead { get { return Hp <= 0; } }
        public IController Controller { get; set; }

        internal void AddAction(Action action)
        {
            Actions.Add(action);
            action.Character = this;
        }

        public TimeSpan FatigatedTime { get; private set; }

        internal bool IsFatigated()
        {
            return FatigatedTime != TimeSpan.Zero;
        }
        internal void Fatigated(TimeSpan exaustingTime)
        {
            FatigatedTime = exaustingTime;
        }


        internal void SetHp(int value)
        {
            Hp = value;
            MaxHp = value;
        }
        internal void SetMp(int value, float rechargeSpeed)
        {
            RealMp = value;
            MaxMp = value;
            MpRechargeSpeed = rechargeSpeed;
        }
        public void ExpendMp(int value)
        {
            RealMp -= value;
        }
        public void Recharge(TimeSpan time)
        {
            RealMp += (float) (MpRechargeSpeed * time.TotalSeconds);
            RealMp = Math.Min(RealMp, MaxMp);
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

        public Vector2 FacingDirection { get; private set; }
        
        public Character(Sprite sprite, IController controller)
        {            
            Sprite = sprite;
            this.DrawRectangle = Sprite.SourceRectangle;
            UpdateRectangle();
            Controller = controller;
            ShootTime = TimeSpan.FromMilliseconds(200);
            Actions = new List<Action>();
        }       

        public override void Update(GameTime gameTime)
        {
            Controller.UpdateState();

            Vector2 v = Controller.Direction();
            Speed = v * MaxSpeed;

            if (v != Vector2.Zero)
            {
                FacingDirection = v;
                if (this.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Facing))
                {
                    (Sprite as IFacingChangeSprite).SetFacing(FacingDirection);
                }
            }

            Recharge(gameTime.ElapsedGameTime);
            foreach (var action in Actions)
            {
                action.Check();
            }
            
            if (IsFatigated())
            {
                FatigatedTime -= gameTime.ElapsedGameTime;
                if(FatigatedTime < TimeSpan.Zero)
                {
                    FatigatedTime = TimeSpan.Zero;
                }
            }

            base.Update(gameTime);            
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
