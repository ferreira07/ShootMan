using Microsoft.Xna.Framework;
using ShootMan.Colision;
using ShootMan.Draw;
using ShootMan.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Move
{
    public abstract class MovingObject : MapObject
    {
        public float MaxSpeed { get; set; }

        public Vector2 Speed { get; set; }

        protected void AttemptToMove(float x, float y)
        {
            float px = Position.X + x;
            float py = Position.Y + y;

            var newPosition = new Rectangle((int)px, (int)py, Width, Height);
            bool hasColision = false;
            List<IColider> temp = new List<IColider>();
            var colisionObjects = Map.ColisionObjects.ToArray();
            foreach (var item in colisionObjects)
            {
                if (item == this) continue;
                if (newPosition.Intersects(item.ColisionRectangle))
                {
                    if(this.ColisionType == EColisionType.Blocking &&
                        item.ColisionType == EColisionType.Blocking)
                    {
                        Vector2 v = MoveTo.Move(x, y, ColisionRectangle, item.ColisionRectangle);
                        if(v != Vector2.Zero) AttemptToMove(v.X, v.Y);
                        hasColision = true;
                        break;
                    }
                    else if (this.ColisionType == EColisionType.Hit &&
                        item.ColisionType == EColisionType.Hit)
                    {
                        // O que fazer quando os 2 objetos são hit??
                    }
                    else
                    {
                        //hit
                        //hasColision = true;
                        OnColide(item);
                    }
                }
            }
            if (!hasColision)
            {
                Position = new Vector2(px, py);
                UpdateRectangle();
            }
        }        

        public void UpdateRectangle()
        {
            DrawRectangle = new Rectangle((int)Position.X + Dx, (int)Position.Y + Dy, DrawRectangle.Width, DrawRectangle.Height);
            ColisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Speed.X != 0 || Speed.Y != 0)
            {
                float x = Speed.X * gameTime.ElapsedGameTime.Milliseconds / 1000;
                float y = -Speed.Y * gameTime.ElapsedGameTime.Milliseconds / 1000;

                AttemptToMove(x, y);
            }
        }

        public virtual void OnColide(IColider c)
        {
        }
    }
}
