using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using Microsoft.Xna.Framework.Media;

namespace Projet_02
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        SoundEffect son;
        SoundEffect son2;
        SoundEffect son3;
        SoundEffectInstance pig;
        SoundEffect son4;
        SoundEffectInstance sHurt;
        SoundEffectInstance hit;
        SoundEffectInstance hurt;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject cochon;
        GameObject GameOver;
        GameObject[] tortue = new GameObject[6];
        GameObject[] pomme = new GameObject[10];
        GameObject[] pipeB = new GameObject[2];
        GameObject[] pipeT = new GameObject[2];
        GameObject wallpaper;
        KeyboardState previousKey;
        int pommeRendu = 0;
        Random deDirection = new Random();
        float timeOfDeath = 0;
        string displayTimeOfDeath = "";
        SpriteFont font;

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
            cochon.isAlive = true;
            cochon.origin.X = cochon.GetRect().Width / 2;
            cochon.origin.Y = cochon.GetRect().Height / 2;
            cochon.lives = 3;

            //Pomme, étant les projectiles de Méchoui le Cochon.
            for (int i = 0; i < pomme.Length; i++)
            {
                pomme[i] = new GameObject();
                pomme[i].sprite = Content.Load<Texture2D>("sPomme.png");
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
                tortue[i].sprite = Content.Load<Texture2D>("sTortue.png");
                tortue[i].origin.X = tortue[i].GetRect().Width / 2;
                tortue[i].origin.Y = tortue[i].GetRect().Height / 2;
                tortue[i].lives = 1;
                tortue[i].speed = 0.8f;
                tortue[i].isAlive = true;

            }

            GameOver = new GameObject();
            GameOver.sprite = Content.Load<Texture2D>("GameOver.jpg");
            GameOver.position = new Vector2(0, 0);


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
            Song song = Content.Load<Song>("Sounds\\sewer");
            MediaPlayer.Play(song);
            son = Content.Load<SoundEffect>("Sounds\\pig");
            son2 = Content.Load<SoundEffect>("Sounds\\hit");
            son3 = Content.Load<SoundEffect>("Sounds\\hurt");
            son4 = Content.Load<SoundEffect>("Sounds\\sHurt");
            hit = son2.CreateInstance();
            pig = son.CreateInstance();
            hurt = son3.CreateInstance();
            sHurt = son4.CreateInstance();
            font = Content.Load<SpriteFont>("Font");

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
            {
                pomme[pommeRendu].isAlive = true;
                pommeRendu += 1;
                pig.Play();
            }

            updateCochon();
            updatePomme();
            updateTortue();

            // TODO: Add your update logic here

            if (pommeRendu > pomme.Length - 1)
                pommeRendu = 0;

            timeOfDeath += (int)gameTime.ElapsedGameTime.TotalSeconds;
            displayTimeOfDeath = Convert.ToString(timeOfDeath);

            cochon.timeSinceLast += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Duration = hasard = temps de chacune des direction
            // timeSinceLast = calculé à partir de la dernière fois que l'on à changé de direction
            // Quand le temps écoulé depuis le dernier changement dépasse la "duration" on calcule un nouveau randomDirection
            for (int i = 0; i < tortue.Length; i++)
            {
                tortue[i].duration = deDirection.Next(1, 10);
                tortue[i].timeSinceLast += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            for (int i = 0; i < tortue.Length; i++)
            {
                if (tortue[i].timeSinceLast > tortue[i].duration)
                {
                    tortue[i].randomDirection = deDirection.Next(1, 16);
                    if (tortue[i].randomDirection > 9)
                        tortue[i].randomDirection = 9;
                    tortue[i].timeSinceLast = 0;
                }
            }


            previousKey = Keyboard.GetState();
            base.Update(gameTime);

        }

        public void updateCochon()
        {
            if (cochon.timeSinceLast > 10)
                cochon.canBeHurt = true;

            if (cochon.lives < 1)
            {
                cochon.isAlive = false;
                cochon.position.X = -500;
                cochon.position.Y = -500;
            }

            cochon.position = cochon.velocity + cochon.position;

            for (int i = 0; i < tortue.Length; i++)
            {
                if (cochon.GetRect().Intersects(tortue[i].GetRect()) && cochon.canBeHurt == true)
                {
                    cochon.canBeHurt = false;
                    cochon.lives -= 1;
                    cochon.timeSinceLast = 0;
                    if (cochon.lives > 0)
                        sHurt.Play();
                    else
                        hurt.Play();


                }
            }
        }
        public void updatePomme()
        {
            {
                for (int i = 0; i < pomme.Length; i++)
                {
                    if (pomme[i].isAlive == true)
                    {
                        pomme[i].position.X = cochon.position.X;
                        pomme[i].position.Y = cochon.position.Y;
                        pomme[i].rotation = cochon.rotation;
                        pomme[i].velocity.X = (float)Math.Cos(pomme[i].rotation) * 10f;
                        pomme[i].velocity.Y = (float)Math.Sin(pomme[i].rotation) * 10f;
                        pomme[i].isAlive = false;
                    }
                    pomme[i].position += pomme[i].velocity;

                    for (int j = 0; j < tortue.Length; j++)
                    {
                        if (pomme[i].GetRect().Intersects(tortue[j].GetRect())&&tortue[j].isAlive==true)
                        {
                            hit.Play();
                        }

                    }
                }
            }
        }
        public void updateTortue()
        {
            for (int i = 0; i < tortue.Length; i++)
            {
                if (tortue[i].isAlive==true)
                {
                    if (tortue[i].position.X + tortue[i].sprite.Width / 2 > fenetre.Width)
                        tortue[i].position.X = fenetre.Width - tortue[i].sprite.Width / 2;
                    if (tortue[i].position.X - tortue[i].sprite.Width / 2 < fenetre.X)
                        tortue[i].position.X = fenetre.X + tortue[i].sprite.Width / 2;
                    if (tortue[i].position.Y + tortue[i].sprite.Height / 2 > fenetre.Height)
                        tortue[i].position.Y = fenetre.Height - tortue[i].sprite.Height / 2;
                    if (tortue[i].position.Y - tortue[i].sprite.Height / 2 < fenetre.Y)
                        tortue[i].position.Y = fenetre.Y + tortue[i].sprite.Height / 2;
                }
                
                for (int j = 0; j < pomme.Length; j++)
                {
                    if (tortue[i].GetRect().Intersects(pomme[j].GetRect()))
                    {
                        tortue[i].lives -= 1;
                        pomme[j].position.X = -500;
                        pomme[j].position.Y = -500;
                    }  
                }
                if (tortue[i].lives < 1)
                    {
                        tortue[i].isAlive = false;
                        tortue[i].position.X = -500;
                        tortue[i].position.Y = -500;
                    }

                switch (tortue[i].randomDirection)
                {
                    case 1:
                        tortue[i].position.X -= tortue[i].speed;
                        break;
                    case 2:
                        tortue[i].position.X -= tortue[i].speed;
                        tortue[i].position.Y -= tortue[i].speed;
                        break;
                    case 3:
                        tortue[i].position.Y -= tortue[i].speed;
                        break;
                    case 4:
                        tortue[i].position.Y -= tortue[i].speed;
                        tortue[i].position.X += tortue[i].speed;
                        break;
                    case 5:
                        tortue[i].position.X += tortue[i].speed;
                        break;
                    case 6:
                        tortue[i].position.X += tortue[i].speed;
                        tortue[i].position.Y += tortue[i].speed;
                        break;
                    case 7:
                        tortue[i].position.Y += tortue[i].speed;
                        break;
                    case 8:
                        tortue[i].position.Y += tortue[i].speed;
                        tortue[i].position.X -= tortue[i].speed;
                        break;
                    case (9):
                        {

                            if (tortue[i].position.X < cochon.position.X)
                            {
                                tortue[i].position.X += tortue[i].speed;
                            }
                            else
                            {
                                tortue[i].position.X -= tortue[i].speed;
                            }


                            if (tortue[i].position.Y < cochon.position.Y)
                            {
                                tortue[i].position.Y += tortue[i].speed;
                            }
                            else
                            {
                                tortue[i].position.Y -= tortue[i].speed;
                            }

                        }
                        break;
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
            //Affichage du cochon
            if (cochon.isAlive == true)
            {
                spriteBatch.Draw(cochon.sprite, cochon.position, null, Color.White, cochon.rotation, cochon.origin, 1f, SpriteEffects.None, 0);
            }

            //Affichage des tortues Leonardo
            for (int i = 0; i < tortue.Length; i++)
            {
                if (tortue[i].isAlive == true)
                {
                    spriteBatch.Draw(tortue[i].sprite, tortue[i].position, null, Color.White, tortue[i].rotation, tortue[i].origin, 1f, SpriteEffects.None, 0);
                }
            }
            //Affichage du projectile
            for (int i = 0; i < pomme.Length; i++)
                spriteBatch.Draw(pomme[i].sprite, pomme[i].position, null, Color.White, pomme[i].rotation, pomme[i].origin, 1f, SpriteEffects.None, 0);

            //Affichage du dessus du tuyau
            for (int i = 0; i <= 1; i++)
            {
                if (pipeB[i].position.X < (fenetre.Width / 2))
                    spriteBatch.Draw(pipeT[i].sprite, pipeT[i].position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else
                    spriteBatch.Draw(pipeT[i].sprite, pipeT[i].position, Color.White);
            }
            //Affichage du GameOver
            if (cochon.isAlive != true)
            {
                spriteBatch.Draw(GameOver.sprite, GameOver.position);
                spriteBatch.DrawString(font, displayTimeOfDeath , new Vector2(100, 100), Color.Black);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
