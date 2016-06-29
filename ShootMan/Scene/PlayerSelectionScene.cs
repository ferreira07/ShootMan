using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShootMan.Map;
using ShootMan.Player;
using ShootMan.Draw;

namespace ShootMan.Scene
{
    public class PlayerSelectionScene : IScene
    {
        public const int MAX_PLAYERS = 4;

        public List<PlayerSelection> Players { get; set; }

        public PlayerSelectionScene()
        {
            Players = new List<PlayerSelection>(4);
        }

        public event EventHandler<IScene> ChangeScene;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(Sprites.GetSprite(ESprite.Title).Texture, new Vector2(100, 100), Color.White);

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (Players.Count > i)
                {
                    spriteBatch.DrawString(ShootMan.Font1, Players[i].Controller.ControllerType.ToString(), new Vector2(100 * i, 200), Color.Black);
                    spriteBatch.DrawString(ShootMan.Font1, Players[i].Type.ToString(), new Vector2(100 * i, 220), Color.Black);
                    spriteBatch.DrawString(ShootMan.Font1, Players[i].Confirmed.ToString(), new Vector2(100 * i, 240), Color.Black);
                }
            }

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            TryAddNewPlayer();
            foreach (var player in Players)
            {
                player.Update();
            }
            if (IsComplete())
            {
                StartGame();
            }
        }

        private bool IsComplete()
        {
            return Players.Count > 1 && (Players.Where(p => !p.Confirmed).Count() == 0);
        }

        private void TryAddNewPlayer()
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                var state = GamePad.GetState(i);
                if (state.IsConnected && state.IsButtonDown(Buttons.Start))
                {
                    bool hasController = HasJoypad(i);
                    if (!hasController)
                    {
                        Players.Add(new PlayerSelection() { Controller = new JoypadController(i) });
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                bool hasController = HasKeybord();
                if (!hasController)
                {
                    Players.Add(new PlayerSelection() { Controller = new KeyboardController() });
                }
            }
        }

        private bool HasKeybord()
        {
            bool hasController = false;
            foreach (var player in Players)
            {
                if (player.Controller.ControllerType == EControllerType.Keyboard)
                {
                    hasController = true;
                }
            }

            return hasController;
        }

        private bool HasJoypad(int i)
        {
            bool hasController = false;
            foreach (var player in Players)
            {
                if (player.Controller.ControllerType == EControllerType.Joypad &&
                    (player.Controller as JoypadController).GamePadIndex == i)
                {
                    hasController = true;
                }
            }
            return hasController;
        }

        private void StartGame()
        {
            BattleMapBuilder builder = new BattleMapBuilder();
            foreach (var item in Players)
            {
                builder.AddCharacter(item.Type, item.Controller);
            }
            var newScene = new BattleScene() { Map = builder.BuildMap() };

            ChangeScene?.Invoke(this, newScene);
        }
    }
}
