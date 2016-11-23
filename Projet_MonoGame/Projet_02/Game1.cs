using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projet_02
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject cochon;
        GameObject[] tortue = new GameObject[6];
        GameObject[] pomme = new GameObject[10];
        GameObject[] pipeB = new GameObject[2];
        GameObject[] pipeT = new GameObject[2];
        GameObject wallpaper;
        KeyboardState previousKey;
        Random dePosition = new Random();

        Rectangle fenetre;
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

            //Wallpaper, étant l'image de fond d'écran.
            wallpaper = new GameObject();
            wallpaper.sprite = Content.Load<Texture2D>("Wallpaper.png");
            wallpaper.position.X = fenetre.X;
            wallpaper.position.Y = fenetre.Y;

            //PipeB, étant le dessous des tuyaux. 
            for (int i = 0; i <= 1; i++)
            {
                pipeB[i] = new GameObject();
                pipeB[i].sprite = Content.Load<Texture2D>("sBottom.png");
                pipeB[i].position.Y = (fenetre.Height / 2) - (pipeB[i].sprite.Height / 2);
                if (i == 0)
                {
                    pipeB[i].position.X = fenetre.X - 86;
                }
                else if (i == 1)
                {
                    pipeB[i].position.X = fenetre.Width - pipeB[i].sprite.Width + 86;
                }
            }

            //PipeT, étant le dessus des tuyaux, positionné pile au dessus des PipeB.
            for (int i = 0; i <= 1; i++)
            {
                pipeT[i] = new GameObject();
                pipeT[i].sprite = Content.Load<Texture2D>("sTop.png");
                pipeT[i].position.Y = pipeB[i].position.Y;
                pipeT[i].position.X = pipeB[i].position.X;
            }

            //Cochon, étant le personnage principale. Peut s'orienter, avancer, reculer, et cracher des pommes. Il possède initialement 10 de vie.
            cochon = new GameObject();
            cochon.sprite = Content.Load<Texture2D>("sCochon.png");
            //cochon.position.X = fenetre.Width / 2;
            //cochon.position.Y = fenetre.Height / 2;
            cochon.position = new Vector2(fenetre.Width / 2, fenetre.Height / 2);

            cochon.origin.X = cochon.GetRect().Width / 2;
            cochon.origin.Y = cochon.GetRect().Height / 2;
            cochon.lives = 10;

            //Pomme, étant les projectiles de Méchoui le Cochon.
            for (int i = 0; i < pomme.Length; i++)
            {
                pomme[i] = new GameObject();
                pomme[i].sprite = Content.Load<Texture2D>("sFil.png");
                pomme[i].isAlive = false;
                pomme[i].origin.X = pomme[i].GetRect().Width / 2 - 60;
                pomme[i].origin.Y = pomme[i].GetRect().Height / 2;
                pomme[i].position.X = -100;
                pomme[i].position.Y = -100;
            }

            //Tortues bleu, Leonardo, elle "spawn" du tuyau gauche ou droit au hasard, peut être présente 6 fois dans l'écran en même temps.
            //Suit Méchoui comme un missile tête chercheuse. 
            for (int i = 0; i < tortue.Length; i++)
            {
                tortue[i] = new GameObject();
                tortue[i].sprite = Content.Load<Texture2D>("sCochon.png");
                tortue[i].position.X = dePosition.Next(fenetre.X, fenetre.Width);
                tortue[i].position.Y = dePosition.Next(fenetre.Y, fenetre.Height);
                tortue[i].origin.X = tortue[i].GetRect().Width / 2;
                tortue[i].origin.Y = tortue[i].GetRect().Height / 2;
                tortue[i].lives = 1;
                tortue[i].speed = 1;
            }


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
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                cochon.rotation -= 0.05f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                cochon.rotation += 0.05f;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cochon.velocity.X = (float)Math.Cos(cochon.rotation) * cochon.tanVelocity;
                cochon.velocity.Y = (float)Math.Sin(cochon.rotation) * cochon.tanVelocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cochon.velocity.X = -((float)Math.Cos(cochon.rotation) * cochon.tanVelocity) / 1.5f;
                cochon.velocity.Y = -((float)Math.Sin(cochon.rotation) * cochon.tanVelocity) / 1.5f;
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cochon.velocity.X = cochon.velocity.X / 1.5f;
                cochon.velocity.Y = cochon.velocity.Y / 1.5f;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && previousKey.IsKeyDown(Keys.Space))
                for (int i = 0; i < pomme.Length; i++)
                {
                    pomme[i].isAlive = true;
                }

            updateCochon();
            updatePomme();
            updateTortue();

            // TODO: Add your update logic here

            previousKey = Keyboard.GetState();
            base.Update(gameTime);

        }

        public void updateCochon()
        {
            cochon.position = cochon.velocity + cochon.position;
        }
        public void updatePomme()
        {
            for (int i = 0; i < pomme.Length; i++)
            {
                if (pomme[i].isAlive == true)
                {
                    pomme[i].position.X = cochon.position.X;
                    pomme[i].position.Y = cochon.position.Y;
                    pomme[i].rotation = cochon.rotation;
                    pomme[i].velocity.X = (float)Math.Cos(pomme[i].rotation) * 20f;
                    pomme[i].velocity.Y = (float)Math.Sin(pomme[i].rotation) * 20f;
                    pomme[i].isAlive = false;
                }
                pomme[i].position += pomme[i].velocity;
            }
        }
        public void updateTortue()
        {
            for(int i =0; i<tortue.Length;i++)
            {
                if (tortue[i].position.X>cochon.position.X)
                {
                    tortue[i].position.X -= tortue[i].speed;
                }
                else
                {
                    tortue[i].position.X += tortue[i].speed;
                }
                if (tortue[i].position.Y > cochon.position.Y)
                {
                    tortue[i].position.Y -= tortue[i].speed;
                }
                else
                {
                    tortue[i].position.Y += tortue[i].speed;
                }
            }
}


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //Affichage du fond d'écran
            spriteBatch.Draw(wallpaper.sprite, wallpaper.position, Color.White);
            //Affichage du bas du tuyeau
            for (int i = 0; i <= 1; i++)
            {
                if (pipeB[i].position.X < (fenetre.Width / 2))
                    spriteBatch.Draw(pipeB[i].sprite, pipeB[i].position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else
                    spriteBatch.Draw(pipeB[i].sprite, pipeB[i].position, Color.White);
            }
            //Affichage du projectil
            for (int i = 0; i < pomme.Length; i++)
                spriteBatch.Draw(pomme[i].sprite, pomme[i].position, null, Color.White, cochon.rotation, pomme[i].origin, 1f, SpriteEffects.None, 0);

            //Affichage du cochon
            spriteBatch.Draw(cochon.sprite, cochon.position, null, Color.White, cochon.rotation, cochon.origin, 1f, SpriteEffects.None, 0);

            //Affichage des tortues Leonardo
            for (int i = 0; i < tortue.Length; i++)
            {
                spriteBatch.Draw(tortue[i].sprite, tortue[i].position);
            }

            //Affichage du dessus du tuyau
            for (int i = 0; i <= 1; i++)
            {
                if (pipeB[i].position.X < (fenetre.Width / 2))
                    spriteBatch.Draw(pipeT[i].sprite, pipeT[i].position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else
                    spriteBatch.Draw(pipeT[i].sprite, pipeT[i].position, Color.White);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
