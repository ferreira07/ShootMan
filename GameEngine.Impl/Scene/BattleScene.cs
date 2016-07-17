using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Move;
using GameEngine.Draw;
using GameEngine.Player;
using GameEngine;
using GameEngine.Scene;
using GameEngine.Map;

namespace GameEngine.Impl.Scene
{
    public class BattleScene : IBattleScene
    {
        public BattleMapFacade Map;
        private EBattleSceneState State = EBattleSceneState.Ready;

        public event EventHandler<IScene> ChangeScene;
        
        private SpriteFont font = Fonts.GetFont(EFontType.Font1);
        private Sprite backSprite = Sprites.GetSprite(ESpriteType.BarBackground);
        private Sprite hpSprite = Sprites.GetSprite(ESpriteType.HpBar);
        private Sprite mpSprite = Sprites.GetSprite(ESpriteType.MpBar);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            IEnumerable<DrawableObject> objs = Map.DrawableObjects.OrderBy(o => o.Position.Y);
            foreach (var item in objs)
            {
                item.Draw(spriteBatch);
            }

            IEnumerable<Character> chars = Map.Characters;
            int dx = (Constants.WIDTH - 40) / chars.Count();
            int px = dx / 2;
            foreach (var c in chars)
            {
                if (!c.IsDead)
                {
                    spriteBatch.Draw(backSprite.Texture, new Rectangle(px, 20, 100, 36), backSprite.SourceRectangle, Color.White);
                    spriteBatch.Draw(hpSprite.Texture, new Rectangle(px, 20, c.Hp*100/c.MaxHp, 18), hpSprite.SourceRectangle, Color.White);
                    spriteBatch.Draw(mpSprite.Texture, new Rectangle(px, 38, c.Mp * 100 / c.MaxMp, 18), mpSprite.SourceRectangle, Color.White);

                    spriteBatch.Draw(c.Sprite.Texture, new Rectangle(px+100, 10, c.Sprite.SourceRectangle.Width, c.Sprite.SourceRectangle.Height), c.Sprite.SourceRectangle, Color.White);

                    spriteBatch.DrawString(font, c.Hp.ToString(), new Vector2(px, 20), Color.Black);
                    spriteBatch.DrawString(font, c.Mp.ToString(), new Vector2(px, 38), Color.Blue);
                }
                px += dx;
            }
            spriteBatch.DrawString(Fonts.GetFont(EFontType.Font1), Map.RemainTime.ToString("mm") + ":" + Map.RemainTime.ToString("ss"), new Vector2(15, 30), Color.Black);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (State == EBattleSceneState.Ready)
            {
                State = EBattleSceneState.Runing;
            }
            else if (State == EBattleSceneState.Runing)
            {
                Map.StartNewFrame(gameTime.ElapsedGameTime);

                if (Map.IsGameOver())
                {
                    State = EBattleSceneState.Ending;
                }
                else
                {
                    foreach (var item in Map.DrawableObjects.ToList())
                    {
                        var moveObject = item as IUpdateableObject;

                        if (moveObject != null)
                        {
                            moveObject.Update(gameTime.ElapsedGameTime);
                        }

                        if (item.Sprite.SpriteChangeType.HasFlag(ESpriteChangeType.Time))
                        {
                            (item.Sprite as ITimeChangeSprite).PassTime(gameTime.ElapsedGameTime);
                        }
                    }
                    Map.VerifyColision();
                    foreach (var control in Map.Characters.Select(c => c.Controller))
                    {
                        if (control.Action(EControllerAction.Pause))
                        {
                            State = EBattleSceneState.Paused;
                        }
                    }
                }
            }
            else if(State == EBattleSceneState.Paused)
            {
                foreach (var control in Map.Characters.Select(c => c.Controller))
                {
                    control.UpdateState();
                    if (control.Action(EControllerAction.Pause) || 
                        control.Action(EControllerAction.Cancel))
                    {
                        State = EBattleSceneState.Runing;
                    }
                }
            }
            else if (State == EBattleSceneState.Ending)
            {
                foreach (var control in Map.Characters.Select(c => c.Controller))
                {
                    control.UpdateState();
                    if (control.Action(EControllerAction.Pause))
                    {
                        EndBattle();
                    }
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
