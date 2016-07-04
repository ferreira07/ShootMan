using GameEngine.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Impl.Scene
{
    public class BattleSceneProxy : IBattleScene
    {
        public event EventHandler<IScene> ChangeScene;

        public BattleScene BattleScene { get; set; }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.BattleScene.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            this.BattleScene.Update(gameTime);
        }
    }
}
