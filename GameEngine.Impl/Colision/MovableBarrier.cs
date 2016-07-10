using GameEngine.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Colision;
using Microsoft.Xna.Framework;
using GameEngine.Draw;

namespace GameEngine.Impl.Colision
{
    public class MovableBarrier : MovingObject
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
        public MovableBarrier(Rectangle position, int hp, TimeChangeSprite risingSprite, Sprite movableSprite)
        {
            _RisingSprite = risingSprite;
            _RisingSprite.AnimateOnce = true;
            _MovableSprite = movableSprite;
            Sprite = risingSprite;
            DrawRectangle = position;
            ColisionRectangle = position;
            Position = new Vector2(position.X, position.Y);
            SetHp(hp);
            _State = EMovableBarrierState.Rising;
        }

        public override void Update(GameTime gameTime)
        {
            if(this._State == EMovableBarrierState.Rising)
            {
                if (_RisingSprite.StopAnimate)
                {
                    Sprite = _MovableSprite;
                    _State = EMovableBarrierState.Barrier;
                }
            }
            base.Update(gameTime);
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

        public int DamageAmmount { get; internal set; }        
    }
}
