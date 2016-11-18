using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Projet_01;

namespace Projet_01
    //version avec changements, pour refaire fonctionner mon github
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
            fond = new GameObject();
            fond.sprite = Content.Load<Texture2D>("fPaille.png");
            fond.position.X = 0;
            fond.position.Y = 0;
            fences = new GameObject();
            fences.isAlive = false;
            fences.sprite = Content.Load<Texture2D>("fFences.png");
            fences.position.X = 0;
            fences.position.Y = 0;
            poule = new GameObject();
            poule.isAlive = true;
            poule.sprite = Content.Load<Texture2D>("sPoule.png");
            poule.position.X = fenetre.X;
            poule.position.Y = fenetre.Height / 2 - poule.sprite.Height / 2;
            poule.velocity = 8;
            poule.vie = 10;
            oeuf = new GameObject();
            araignee = new GameObject();
            araignee.isAlive = true;
            araignee.sprite = Content.Load<Texture2D>("sAraignee.png");
            araignee.position.X = fenetre.Width / 2 - araignee.sprite.Width / 2;
            araignee.position.Y = fenetre.Y + 128;
            araignee.velocity = 2;
            araignee.vie = 10;
            fil = new GameObject();
            fil.velocity = 10;
            fil.isAlive = true;
            fil.sprite = Content.Load<Texture2D>("sFil.png");
            fil.position.X = araignee.position.X + araignee.sprite.Width / 2 - fil.sprite.Width;
            fil.position.Y = araignee.sprite.Height / 2;
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                poule.position.Y -= poule.velocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                poule.position.Y += poule.velocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                poule.position.X -= poule.velocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                poule.position.X += poule.velocity;
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
            if (poule.vie <=0)
            {
                poule.isAlive = false;
            }
        }
        public void updateAraignee()
        {
            araignee.position.X -= araignee.velocity;

            if (sortir == true)
            {
                if (araignee.position.X < fenetre.X || araignee.position.X + araignee.sprite.Width > fenetre.Width)
                {
                    araignee.velocity = araignee.velocity * -1;
                }
            }
            else
            {
                if (araignee.position.X < fenetre.X + 50 || araignee.position.X + araignee.sprite.Width > fenetre.Width - 50)
                {
                    araignee.velocity = araignee.velocity * -1;
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
                fences.sprite = Content.Load<Texture2D>("fFences.png");
            }
            else
            {
                fences.isAlive = false;
            }

        }
        public void updateFil()
        {
            fil.tempsMort--;

            if (fil.isAlive == true)
            {
                fil.position.Y += fil.velocity;
            }
            if (fil.position.Y > fenetre.Height || fil.isAlive ==false)
            {
                fil.position.X = araignee.position.X + araignee.sprite.Width / 2 - fil.sprite.Width;
                fil.position.Y = araignee.sprite.Height;
                fil.isAlive = true;
            }
            if (fil.isAlive)
            {
                if (fil.GetRect().Intersects(poule.GetRect()))
                {
                    fil.isAlive = false;
                    poule.vie--;
                    fil.tempsMort = 180;
                }
            }
            if (fil.tempsMort<=0)
                {
                fil.isAlive = true;
                }
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
            spriteBatch.Draw(fond.sprite, fond.position, Color.Teal);
            if (fences.isAlive == true)
            {
                spriteBatch.Draw(fences.sprite, fences.position, Color.White);
            }
            if (poule.isAlive == true)
            {
                spriteBatch.Draw(poule.sprite, poule.position, Color.White);
            }
            if (fil.isAlive ==true)
            {
                spriteBatch.Draw(fil.sprite, fil.position, Color.White);
            }
            if (araignee.isAlive ==true)
            {
                spriteBatch.Draw(araignee.sprite, araignee.position, Color.White);

            }


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
