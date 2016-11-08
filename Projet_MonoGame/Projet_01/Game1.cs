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
        GameObject cible;
        Rectangle fenetre;
        int xWindow = 0;
        int yWindow = 0;
        public bool sortir = true;


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
            cible = new GameObject();
            cible.position.Y = 0;
            cible.position.X = xWindow / 2;
            cible.sprite = Content.Load<Texture2D>("CibleOmbre.png");
            mario.estVivant = true;
            mario.position.X = 200;
            mario.position.Y = 000;
            mario.sprite = Content.Load<Texture2D>("poule.png");




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

            if (Keyboard.GetState().IsKeyDown(Keys.P))

                sortir = false;

            if (Keyboard.GetState().IsKeyDown(Keys.O))

                sortir = true;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mario.position.X -= 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                mario.position.Y -= 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mario.position.Y += 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mario.position.X += 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                mario.sprite = Content.Load<Texture2D>("Oeuf.png");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                mario.sprite = Content.Load<Texture2D>("poule.png");
            }
            if (sortir == true)
            {

                if ((mario.position.X + mario.sprite.Width) < 0)
                {
                    mario.position.X = xWindow;
                }
                if ((mario.position.X) > xWindow)
                {
                    mario.position.X = -mario.sprite.Width;
                }
                if ((mario.position.Y) > yWindow)
                {
                    mario.position.Y = -mario.sprite.Height;
                }
                if ((mario.position.Y + mario.sprite.Height) < 0)
                {
                    mario.position.Y = yWindow;
                }
            }
            else
            {
                if ((mario.position.X + mario.sprite.Width) >= xWindow)
                {
                    mario.position.X = xWindow - mario.sprite.Width;
                }
                if ((mario.position.X) <= 0)
                {
                    mario.position.X = 0;
                }
                if ((mario.position.Y) < 0)
                {
                    mario.position.Y = 0;
                }
                if ((mario.position.Y + mario.sprite.Height) > yWindow)
                {
                    mario.position.Y = yWindow- mario.sprite.Height;
                }
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
            spriteBatch.Draw(cible.sprite, cible.position, Color.White);







            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
