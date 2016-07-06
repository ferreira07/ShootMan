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
        public bool IsDead { get { return Hp <= 0; } }

        public List<Action> Actions { get; private set; }
        public  void AddAction(Action action)
        {
            Actions.Add(action);
            action.Character = this;
        }
        public IController Controller { get; set; }

        public void SetHp(int value)
        {
            Hp = value;
            MaxHp = value;
        }
        public void SetMp(int value, float rechargeSpeed)
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
            RealMp += (float)(MpRechargeSpeed * time.TotalSeconds);
            RealMp = Math.Min(RealMp, MaxMp);

            if (IsFatigated())
            {
                FatigatedTime -= time;
                if (FatigatedTime < TimeSpan.Zero)
                {
                    FatigatedTime = TimeSpan.Zero;
                }
            }
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
        public ChargingAction ChargingAction { get; set; }

        internal bool CanAct()
        {
            return !IsFatigated() && ChargingAction == null;
        }
        
        private TimeSpan StartChargeShoot { get; set; }

        private EColisionLayer _ColisionLayer { get; set; }

        public override EColisionLayer ColisionLayer
        {
            get
            {
                return _ColisionLayer;
            }
        }

        public Vector2 FacingDirection { get; private set; }

        public Character(Sprite sprite, IController controller)
        {
            Sprite = sprite;
            this.DrawRectangle = Sprite.SourceRectangle;
            UpdateRectangle();
            Controller = controller;
            Actions = new List<Action>();
        }

        public override void Update(GameTime gameTime)
        {
            Controller.UpdateState();
            Recharge(gameTime.ElapsedGameTime);
            CheckActions(gameTime.ElapsedGameTime);            

            base.Update(gameTime);
        }

        private void SetFacing(Vector2 v)
        {
            FacingDirection = v;
            if (this.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Facing))
            {
                (Sprite as IFacingChangeSprite).SetFacing(FacingDirection);
            }
        }

        private void CheckActions(TimeSpan elapsedGameTime)
        {
            if(ChargingAction!= null)
            {
                ChargingAction.RemainTime -= elapsedGameTime;
                if(ChargingAction.RemainTime <= TimeSpan.Zero)
                {
                    ChargingAction.Action.Execute();
                    ChargingAction = null;
                }
                Speed = Vector2.Zero;
            }
            else
            {
                Vector2 v = Controller.Direction();
                Speed = v * MaxSpeed;

                if (v != Vector2.Zero)
                {
                    SetFacing(v);
                }

                foreach (var action in Actions)
                {
                    action.Check();
                }
            }
        }

        public override void OnColide(IColider c)
        {
            if (c is IPowerUp)
            {
                (c as IPowerUp).Aplly(this);
            }
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
