using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShootMan.Colision;
using ShootMan.Draw;
using ShootMan.Map;
using ShootMan.Move;
using ShootMan.Player;
using System.Collections.Generic;
using System.Linq;

namespace ShootMan
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ShootMan : Game
    {
        public static Texture2D textureRED;
        public static Texture2D textureBLUE;
        public static Texture2D BulletTexture;
        public static float SpeedBase = 100;
        public static SpriteFont Font1;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public IScene Scene { get; set; }

        public BattleMap Map { get; set; }

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

            Font1 = Content.Load<SpriteFont>("arial");
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Sprites.Load(Content);

            BattleMapBuilder builder = new BattleMapBuilder();
            builder.AddCharacter(ECharacterType.Fulano, new JoypadController(0));
            builder.AddCharacter(ECharacterType.Beltrano, new JoypadController(1));
            builder.AddCharacter(ECharacterType.Siclano, new KeyboardController());
            Scene = new BattleScene() { Map = builder.BuildMap() };
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureRED = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            textureRED.SetData(new Color[] { Color.DarkRed });
            textureBLUE = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            textureBLUE.SetData(new Color[] { Color.Blue });
            BulletTexture = Content.Load<Texture2D>("Images\\bala1");

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
