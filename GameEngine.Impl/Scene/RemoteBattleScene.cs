using GameEngine.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Player;

namespace GameEngine.Impl.Scene
{
    public class RemoteBattleScene : IBattleScene
    {
        public List<Character> LocalPlayers { get; set; }


        public event EventHandler<IScene> ChangeScene;


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //GetStatus();
            //
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            //SetControllerStatus();

            throw new NotImplementedException();
        }
    }
}
