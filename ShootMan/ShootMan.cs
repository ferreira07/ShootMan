using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShootMan.Colision;
using ShootMan.Draw;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Map Map { get; set; }

        public const int WIDTH = 800;
        public const int HEIGHT = 600;
        SpriteFont Font1;

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

            Character p = CharacterFactory.CreateCharacter(ECharacterType.Fulano, new Vector2(100, 150), new JoypadController(0));
            Character p1 = CharacterFactory.CreateCharacter(ECharacterType.Beltrano, new Vector2(200, 150), new JoypadController(1));
            Character p2 = CharacterFactory.CreateCharacter(ECharacterType.Siclano, new Vector2(300, 150), new KeyboardController());

            Map = new Map();
            Map.Add(p);
            Map.Add(p1);
            Map.Add(p2);

            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Green });
            Sprite s = new Sprite() { Texture = texture, SourceRectangle = new Rectangle(0, 0, 1, 1) }; 
            for (int i = 0; i < 25; i++)
            {
                Map.Add(new Wall(s, new Rectangle(32 * i, 90, 32, 32)));
                Map.Add(new Wall(s, new Rectangle(32 * i, 90+15*32, 32, 32)));
            }
            for (int i = 0; i < 14; i++)
            {
                Map.Add(new Wall(s, new Rectangle(0, 122 + 32 * i, 32, 32)));
                Map.Add(new Wall(s, new Rectangle(24*32, 122 +32 * i, 32, 32)));
            }
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
            foreach (var item in Map.MapObjects.Where(d=> d is MovingObject).Select(d => d as MovingObject).ToList())
            {
                item.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin();
            IEnumerable<DrawableObject> objs = Map.MapObjects.Where(c => c is DrawableObject).Select(o => o as DrawableObject).OrderBy(o => o.Position.Y);
            foreach (var item in objs)
            {
                item.Draw(spriteBatch);
            }

            IEnumerable<Character> chars = Map.Characters;
            int dx = WIDTH / chars.Count();
            int px = dx / 2;
            foreach (var c in chars)
            {
                if(!c.IsDead)
                    spriteBatch.DrawString(Font1, c.Hp.ToString(), new Vector2(px, 30), Color.Black);
                px += dx;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
