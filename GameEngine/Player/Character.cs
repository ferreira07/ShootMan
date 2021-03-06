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
using GameEngine.Combat;

namespace GameEngine.Player
{
    public class Character : MovingObject
    {
        public override EColisionType ColisionType
        {
            get { return EColisionType.Blocking; }
        }

        public int Mp { get { return (int)Math.Floor(RealMp); } }
        public float RealMp { get; set; }
        public float MpRechargeSpeed { get; set; }
        public int MaxMp { get; set; }
        public bool IsDead { get { return Hp <= 0; } }

        public List<Action> Actions { get; private set; }
        public void AddAction(Action action)
        {
            Actions.Add(action);
            action.Character = this;
        }
        public IController Controller { get; set; }

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

            if (IsActFatigated())
            {
                FatigatedActionTime -= time;
                if (FatigatedActionTime < TimeSpan.Zero)
                {
                    FatigatedActionTime = TimeSpan.Zero;
                }
            }
            if (IsMoveFatigated())
            {
                FatigatedMoveTime -= time;
                if (FatigatedMoveTime < TimeSpan.Zero)
                {
                    FatigatedMoveTime = TimeSpan.Zero;
                }
            }
        }

        public TimeSpan FatigatedActionTime { get; private set; }
        public TimeSpan FatigatedMoveTime { get; private set; }

        public bool IsActFatigated()
        {
            return FatigatedActionTime != TimeSpan.Zero;
        }
        public bool IsMoveFatigated()
        {
            return FatigatedMoveTime != TimeSpan.Zero;
        }
        public void Fatigated(TimeSpan exaustingActionTime, TimeSpan exaustingMoveTime)
        {
            FatigatedActionTime = exaustingActionTime;
            FatigatedMoveTime = exaustingMoveTime;
        }
        public ChargingAction ChargingAction { get; set; }

        internal bool CanAct()
        {
            return !IsActFatigated() && ChargingAction == null;
        }

        private TimeSpan StartChargeShoot { get; set; }

        private EColisionLayer _ColisionLayer { get; set; }
        public void SetColisionLayer(EColisionLayer value)
        {
            _ColisionLayer = value;
        }
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
            Status = new Dictionary<EStatusType, Combat.Status>();
        }

        public override void Update(TimeSpan elapsedGameTime)
        {
            Controller.UpdateState();
            Recharge(elapsedGameTime);
            CheckActions(elapsedGameTime);
            UpdateStatus(elapsedGameTime);
        }

        private void UpdateStatus(TimeSpan elapsedGameTime)
        {
            List<EStatusType> toRemove = new List<EStatusType>();
            foreach (var item in Status.ToArray())
            {
                if (item.Value.PassTime(elapsedGameTime))
                {
                    Status.Remove(item.Key);
                }
            }
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
            if (ChargingAction != null)
            {
                ChargingAction.RemainTime -= elapsedGameTime;
                if (ChargingAction.RemainTime <= TimeSpan.Zero)
                {
                    ChargingAction.Action.Execute();
                    ChargingAction = null;
                }
                Speed = Vector2.Zero;
            }
            else
            {
                if (!IsMoveFatigated())
                {
                    Vector2 v = Controller.Direction();
                    if (v.Length() > 1) v.Normalize();
                    Speed = v * MaxSpeed;
                    if (v != Vector2.Zero) SetFacing(v);
                    Map.Move(this, elapsedGameTime);
                }
                if (!IsActFatigated())
                {
                    foreach (var action in Actions)
                    {
                        action.Check();
                    }
                }
            }
        }

        public Dictionary<EStatusType, Status> Status { get; set; }
        public override void AddStatus(Status status)
        {
            Status s;
            if (Status.TryGetValue(status.StatusType, out s))
            {
                if (s.RemainTime < status.RemainTime)
                {
                    s.RemainTime = status.RemainTime;
                }
                if (s.TimeCicleAttack.DamageAmmount < status.TimeCicleAttack.DamageAmmount)
                {
                    s.TimeCicleAttack = status.TimeCicleAttack;
                }
            }
            else
            {
                Status.Add(status.StatusType, status);
                status.AffectedObject = this;
            }
        }
    }
}
