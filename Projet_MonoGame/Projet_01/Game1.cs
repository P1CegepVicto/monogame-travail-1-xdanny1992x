using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet_01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject mario;
        Rectangle fenetre;
        int xWindow = 0;
        int yWindow = 0;


        public Game1()
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
            // TODO: Add your initialization logic here

            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            xWindow = graphics.GraphicsDevice.DisplayMode.Width;
            yWindow = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mario = new GameObject();
            mario.estVivant = true;
            mario.position.X = 200;
            mario.position.Y = 000;
            mario.sprite = Content.Load<Texture2D>("poule_small.png");




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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mario.position.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                mario.position.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mario.position.Y += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mario.position.X += 500;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                mario.sprite = Content.Load<Texture2D>("Oeuf.png");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                mario.sprite = Content.Load<Texture2D>("poule_small.png");
            }
            if ((mario.position.X+76) < 0)
            {
                mario.position.X = xWindow;
            }
            if ((mario.position.X) > xWindow)
            {
                mario.position.X = 0;
            }
            if ((mario.position.Y + 198) > yWindow)
            {
                mario.position.Y = 0;
            }
            if ((mario.position.Y) < 0)
            {
                mario.position.X = yWindow;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(mario.sprite, mario.position, Color.White);





        
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
