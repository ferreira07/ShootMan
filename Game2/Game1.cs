using GameEngine.Draw;
using GameEngine.Impl.Colision;
using GameEngine.Impl.Map.Obstacle;
using GameEngine.Impl.Scene;
using GameEngine.Map;
using GameEngine.Player;
using GameEngine.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public IScene Scene { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Sprites.Load(Content, "");
            Fonts.Load(Content);

            //Scene = new PlayerSelectionScene();
            BattleScene bs = new BattleScene();

            BattleMapFacadeBuilder builder = new BattleMapFacadeBuilder();
            builder.AddCharacter(ECharacterType.Fulano, new KeyboardController(0));
            builder.AddCharacter(ECharacterType.Fulano, new KeyboardController(1));
            builder.AddBarrierFactory(new BarrierFactory());
            bs = new BattleScene() { Map = builder.BuildMap() };
            Scene = bs;


            Scene.ChangeScene += changeScene;

            base.Initialize();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            
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
