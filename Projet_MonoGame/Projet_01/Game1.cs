using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Projet_01;

namespace Projet_01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject poule;
        GameObject araignee;
        GameObject fil;
        GameObject oeuf;
        GameObject fond;
        GameObject fences;
        Rectangle fenetre;
        int xWindow = 0;
        int yWindow = 0;
        public bool sortir = true;
        //SoundEffect son;
        //SoundEffectInstance vent;

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
            fond = new GameObject();
            fond.sprite = Content.Load<Texture2D>("fPaille.png");
            fond.position.X = 0;
            fond.position.Y = 0;
            fences = new GameObject();
            fences.sprite = Content.Load<Texture2D>("fFences.png");
            fences.position.X = 0;
            fences.position.Y = 0;
            poule = new GameObject();
            poule.isAlive = true;
            poule.sprite = Content.Load<Texture2D>("sPoule.png");
            poule.position.X = fenetre.X;
            poule.position.Y = fenetre.Height / 2 - poule.sprite.Height / 2;
            poule.velocity.X = 4;
            poule.velocity.Y = 4;
            oeuf = new GameObject();
            araignee = new GameObject();
            araignee.isAlive = true;
            araignee.sprite = Content.Load<Texture2D>("sAraignee.png");
            araignee.position.X = fenetre.Width / 2 - araignee.sprite.Width / 2;
            araignee.position.Y = fenetre.Y + 128;
            araignee.velocity.X = 5;
            fil = new GameObject();
            //Song son = Content.Load<Song>("Sounds\\Windy.wav");
            //MediaPlayer.Play(son);





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

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                poule.position.Y -= poule.velocity.Y;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                poule.position.Y += poule.velocity.Y;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                poule.position.X -= poule.velocity.X;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                poule.position.X += poule.velocity.X;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                sortir = true;
            if (Keyboard.GetState().IsKeyDown(Keys.O))
                sortir = false;

            updatePoule();
            updateAraignee();
            updateFences();
            updateFil();
            updateOeuf();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void updatePoule()
        {
            if (sortir == true)
            {
                if (poule.position.X > fenetre.Width)
                {
                    poule.position.X = fenetre.X - poule.sprite.Width;
                }
                if (poule.position.X + poule.sprite.Width < fenetre.X)
                {
                    poule.position.X = fenetre.Width;
                }
                if (poule.position.Y > fenetre.Height)
                {
                    poule.position.Y = fenetre.Y - poule.sprite.Height;
                }
                if (poule.position.Y + poule.sprite.Height < fenetre.Y)
                {
                    poule.position.Y = fenetre.Height;
                }
            }
            else
            {
                if (poule.position.X < fenetre.X + 50)
                {
                    poule.position.X = fenetre.X + 50;
                }
                if (poule.position.X + poule.sprite.Width > fenetre.Width - 50)
                {
                    poule.position.X = fenetre.Width - (50 + poule.sprite.Width);
                }
                if (poule.position.Y < fenetre.Y + 20)
                {
                    poule.position.Y = fenetre.Y + 20;
                }
                if (poule.position.Y + poule.sprite.Height > fenetre.Height - 20)
                {
                    poule.position.Y = fenetre.Height - (20 + poule.sprite.Height);
                }
            }
        }
        public void updateAraignee()
        {
            araignee.position.X -= araignee.velocity.X;

            if (sortir == true)
            {
                if (araignee.position.X < fenetre.X || araignee.position.X + araignee.sprite.Width > fenetre.Width)
                {
                    araignee.velocity.X = araignee.velocity.X * -1;
                }
            }
            else
            {
                if (araignee.position.X < fenetre.X + 50 || araignee.position.X + araignee.sprite.Width > fenetre.Width - 50)
                {
                    araignee.velocity.X = araignee.velocity.X * -1;
                }
                if (araignee.position.X < fenetre.X + 50)
                {
                    araignee.position.X = fenetre.X + 50;
                }
                if (araignee.position.X + araignee.sprite.Width > fenetre.Width - 50)
                {
                    araignee.position.X = fenetre.Width - (50 + araignee.sprite.Width);
                }
            }
        }
        public void updateFences()
        {
            if (sortir == false)
            {
                fences.isAlive = true;
            }
            else
            {
                fences.isAlive = false;
            }
        }
        public void updateFil()
        {

        }
        public void updateOeuf()
        {

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(fond.sprite, fond.position, Color.LightGreen);
            if (fences.isAlive == true)
            {
                spriteBatch.Draw(fences.sprite, fences.position, Color.White);
            }
            spriteBatch.Draw(poule.sprite, poule.position, Color.White);
            spriteBatch.Draw(araignee.sprite, araignee.position, Color.White);



            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
