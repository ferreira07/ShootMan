using GameEngine.Colision;
using GameEngine.Draw;
using GameEngine.Move;
using GameEngine.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Map
{
    public partial class BattleMap
    {
        public IColisionStrategyFactory ColisionStrategyFactory { get; set; }
        /// <summary>
        /// Executar movimento, para evitar colisões entre objetos do tipo bloking
        /// </summary>
        /// <param name="obj"></param>
        public void Move(MovingObject obj, TimeSpan elapsedGameTime)
        {
            Vector2 speed = obj.Speed * elapsedGameTime.Milliseconds / 1000 * new Vector2(1, -1);

            if (obj.ColisionType == EColisionType.Blocking)
            {
                TryMove(obj, speed);
            }
            else
            {
                ExecuteMove(obj, speed);
            }
        }

        public void VerifyColision()
        {
            foreach (var item1 in ColisionObjects.Where(c => c.ColisionType == EColisionType.Hit).ToArray())
            {
                foreach (var item2 in ColisionObjects.ToArray())
                {
                    if (item1 == item2) continue;


                    if (item1.ColisionRectangle.Intersects(item2.ColisionRectangle))
                    {
                        ColisionStrategyFactory.Create(item1, item2)?.ProcessColision();
                    }
                }
            }
        }

        #region private methods

        private void ExecuteMove(MovingObject obj, Vector2 speed)
        {
            //só mover se tiver alguma velocidade
            if (speed == Vector2.Zero) return;

            obj.Position += speed;
            if (obj.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Move))
            {
                (obj.Sprite as IMoveChangeSprite).Move(speed.Length());
            }
            obj.UpdateRectangle();
        }

        private void TryMove(MovingObject obj, Vector2 speed)
        {
            //só tentar mover se tiver alguma velocidade
            if (speed == Vector2.Zero) return;

            //TODO Calcular nova posição
            float px = obj.ColisionRectangle.X + speed.X;
            float py = obj.ColisionRectangle.Y + speed.Y;

            Rectangle newPosition = new Rectangle((int)px, (int)py, obj.ColisionRectangle.Width, obj.ColisionRectangle.Height);

            bool colide = false;
            List<Vector2> sugestedPositions = new List<Vector2>();
            foreach (var item in ColisionObjects.Where(c => c!= obj && c.ColisionType == EColisionType.Blocking))
            {
                if (item.ColisionRectangle.Intersects(newPosition))
                {
                    colide = true;
                    // Calcular um posição sugerida
                    sugestedPositions.Add(MoveTo.Move(speed.X, speed.Y, obj.ColisionRectangle, item.ColisionRectangle));
                }
            }

            if (colide)
            {
                //Verificar qual posição sugerida se desloca mais
                //tentar novamente andar nessa direção;
                Vector2 newSpeed = sugestedPositions.OrderByDescending(v => v.Length()).First();
                if (newSpeed.Length() < speed.Length())
                {
                    TryMove(obj, newSpeed);
                }
            }
            else
            {
                ExecuteMove(obj, speed);
            }
        }       

        #endregion
    }
}
