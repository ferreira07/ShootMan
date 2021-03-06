﻿using GameEngine.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using Microsoft.Xna.Framework;
using GameEngine.Draw;
using GameEngine.Combat;

namespace GameEngine.Impl.Map.Obstacle
{
    public class MovableBarrier : MovingObject, IAttackContainer, IDefensesContainer
    {
        private enum EMovableBarrierState
        {
            Rising,
            Moving,
            Barrier
        }
        private EMovableBarrierState _State;
        private TimeChangeSprite _RisingSprite;
        private Sprite _MovableSprite;
        public MovableBarrier(RectangleF position, int hp, TimeChangeSprite risingSprite, Sprite movableSprite)
        {
            _RisingSprite = risingSprite;
            _RisingSprite.AnimateOnce = true;
            _MovableSprite = movableSprite;
            Sprite = risingSprite;
            DrawRectangle = position.ToRectangle();
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
            Width = DrawRectangle.Width;
            Height = DrawRectangle.Height;
            SetHp(hp);
            _State = EMovableBarrierState.Rising;
        }

        public override void Update(TimeSpan elapsedGameTime)
        {
            if(this._State == EMovableBarrierState.Rising)
            {
                if (_RisingSprite.StopAnimate)
                {
                    Sprite = _MovableSprite;
                    _State = EMovableBarrierState.Barrier;
                }
            }
            base.Update(elapsedGameTime);
        }

        public void StartMove(Vector2 facingDirection)
        {
            if (facingDirection == Vector2.Zero) return;

            facingDirection.Normalize();
            Speed = facingDirection * MaxSpeed;
            if (this.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Facing))
            {
                (Sprite as IFacingChangeSprite).SetFacing(facingDirection);
            }
            this._State = EMovableBarrierState.Moving;
        }


        public override EColisionLayer ColisionLayer
        {
            get
            {
                return EColisionLayer.Ground | EColisionLayer.Floating;
            }
        }

        public override EColisionType ColisionType
        {
            get
            {
                return _State == EMovableBarrierState.Barrier ? EColisionType.Blocking : EColisionType.Hit;
            }
        }

        public Attack GetAttack()
        {
            return Attack;
        }
        public Attack Attack { get; internal set; }
    }
}
