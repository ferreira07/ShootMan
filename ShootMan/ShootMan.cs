using GameEngine.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Map;
using GameEngine.Move;
using GameEngine.Player;
using GameEngine.Scene;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Impl.Scene;
using System;

namespace ShootMan
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ShootMan : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public IScene Scene { get; set; }
        
        public const int WIDTH = 800;
        public const int HEIGHT = 600;

        public ShootMan()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Sprites.Load(Content);
            Fonts.Load(Content);
            Scene = new PlayerSelectionScene();

            Scene.ChangeScene += changeScene;
        }
        private void changeScene(object sender, IScene scene)
        {
            Scene.ChangeScene -= changeScene;
            Scene = scene;
            Scene.ChangeScene += changeScene;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Scene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Scene.Draw(spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
