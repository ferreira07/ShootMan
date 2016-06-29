using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShootMan.Move;
using ShootMan.Draw;
using ShootMan.Player;

namespace ShootMan.Scene
{
    public class BattleScene : IScene
    {
        public BattleMap Map;

        private bool IsGameOver()
        {
            
            return !(Map.Characters.Where(c => c.Hp > 0).Count() > 1) || Map.RemainTime == TimeSpan.Zero;
        }

        public event EventHandler<IScene> ChangeScene;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            IEnumerable<DrawableObject> objs = Map.MapObjects.Where(c => c is DrawableObject).Select(o => o as DrawableObject).OrderBy(o => o.Position.Y);
            foreach (var item in objs)
            {
                item.Draw(spriteBatch);
            }

            IEnumerable<Character> chars = Map.Characters;
            int dx = (ShootMan.WIDTH - 40) / chars.Count();
            int px = dx / 2;
            foreach (var c in chars)
            {
                if (!c.IsDead)
                    spriteBatch.DrawString(ShootMan.Font1, c.Hp.ToString(), new Vector2(px, 30), Color.Black);
                px += dx;
            }
            spriteBatch.DrawString(ShootMan.Font1, Map.RemainTime.ToString("mm") + ":" + Map.RemainTime.ToString("ss"), new Vector2(15, 30), Color.Black);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Map.PassTime(gameTime.ElapsedGameTime);
            if (IsGameOver())
            {
                EndBattle();
            }
            else
            {

                foreach (var item in Map.MapObjects.Where(d => d is MovingObject).Select(d => d as MovingObject).ToList())
                {
                    item.Update(gameTime);
                }
            }
        }

        private void EndBattle()
        {
            var newScene = new PlayerSelectionScene();
            ChangeScene?.Invoke(this, newScene);
        }
    }
}
