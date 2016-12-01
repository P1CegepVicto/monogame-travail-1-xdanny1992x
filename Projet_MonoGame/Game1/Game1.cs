using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using Microsoft.Xna.Framework.Media;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Rectangle fenetre;
        GameObject wallpaper;
        GameObject temple;
        GameObject cloudBL;
        GameObject cloudBR;
        GameObject cloudML;
        GameObject cloudMR;
        GameObject cloudFL;
        GameObject cloudFR;
        GameObject cage;
        Player juan;
        Mechanism gear;
        Mechanism pulley;
        Texture2D rope;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



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
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);

            //wallpaper
            wallpaper = new GameObject();
            wallpaper.sprite = Content.Load<Texture2D>("Wallpaper.png");
            wallpaper.position.X = fenetre.X;
            wallpaper.position.Y = fenetre.Y;

            //temple
            temple = new GameObject();
            temple.sprite = Content.Load<Texture2D>("TempleSans.png");
            temple.position.X = fenetre.X;
            temple.position.Y = fenetre.Y + 90;

            //cloudBL
            cloudBL = new GameObject();
            cloudBL.sprite = Content.Load<Texture2D>("Clouds_backL.png");
            cloudBL.position.X = fenetre.X;
            cloudBL.position.Y = fenetre.Y;
            cloudBL.speed = 0.225f;

            //cloudBR
            cloudBR = new GameObject();
            cloudBR.sprite = Content.Load<Texture2D>("Clouds_backR.png");
            cloudBR.position.X = fenetre.Width;
            cloudBR.position.Y = fenetre.Y;
            cloudBR.speed = 0.225f;

            //cloudML
            cloudML = new GameObject();
            cloudML.sprite = Content.Load<Texture2D>("Clouds_middleL.png");
            cloudML.position.X = fenetre.X;
            cloudML.position.Y = fenetre.Y;
            cloudML.speed = 0.45f;

            //cloudMR
            cloudMR = new GameObject();
            cloudMR.sprite = Content.Load<Texture2D>("Clouds_middleR.png");
            cloudMR.position.X = fenetre.Width;
            cloudMR.position.Y = fenetre.Y;
            cloudMR.speed = 0.45f;

            //cloudFL
            cloudFL = new GameObject();
            cloudFL.sprite = Content.Load<Texture2D>("Clouds_frontL.png");
            cloudFL.position.X = fenetre.X;
            cloudFL.position.Y = fenetre.Y;
            cloudFL.speed = 0.9f;

            //cloudFR
            cloudFR = new GameObject();
            cloudFR.sprite = Content.Load<Texture2D>("Clouds_frontR.png");
            cloudFR.position.X = fenetre.Width;
            cloudFR.position.Y = fenetre.Y;
            cloudFR.speed = 0.9f;

            //Cage
            cage = new GameObject();
            cage.sprite = Content.Load<Texture2D>("Cage.png");
            cage.position.X = 472;
            cage.position.Y = 256;
            cage.speed = 3;
            cage.height = 0;

            //Juan
            juan = new Player();
            juan.objetState = Player.etats.idle;
            juan.position = new Rectangle((int)(cage.position.X + 24), (int)(cage.position.Y + 44), 130, 144);   //Position initiale de Juan
            juan.sprite = Content.Load<Texture2D>("IndianSSheet.png");
            juan.spriteAfficher = juan.tabIdle[juan.idleState];

            //Gear
            gear = new Mechanism();
            gear.objetState = Mechanism.etats.idle;
            gear.position = new Rectangle(226, 336, 100, 36);
            gear.sprite = Content.Load<Texture2D>("Gear_SpriteSheet.png");
            gear.spriteAfficher = gear.tabGear[1];

            //Pulley
            pulley = new Mechanism();
            pulley.objetState = Mechanism.etats.idle;
            pulley.position = new Rectangle(430, 204, 98, 36);
            pulley.sprite = Content.Load<Texture2D>("Pulley_SpriteSheet.png");
            pulley.spriteAfficher = pulley.tabPulley[1];

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



            rope = Content.Load<Texture2D>("Rope.png");

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

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                juan.objetState = Player.etats.shoot;
            }
            else
            {
                juan.objetState = Player.etats.idle;
            }

            updateCloudBL();
            updateCloudBR();
            updateCloudML();
            updateCloudMR();
            updateCloudFL();
            updateCloudFR();
            updateCage();
            updateJuan();
            updateMechanism();

            base.Update(gameTime);

        }

        public void updateCloudBL()
        {
            cloudBL.position.X -= cloudBL.speed;

            if (cloudBL.position.X < -fenetre.Width)
            {
                cloudBL.position.X = fenetre.Width;
            }
        }

        public void updateCloudBR()
        {
            cloudBR.position.X -= cloudBR.speed;

            if (cloudBR.position.X < -fenetre.Width)
            {
                cloudBR.position.X = fenetre.Width;
            }
        }

        public void updateCloudML()
        {
            cloudML.position.X -= cloudML.speed;

            if (cloudML.position.X < -fenetre.Width)
            {
                cloudML.position.X = fenetre.Width;
            }
        }

        public void updateCloudMR()
        {
            cloudMR.position.X -= cloudMR.speed;

            if (cloudMR.position.X < -fenetre.Width)
            {
                cloudMR.position.X = fenetre.Width;
            }
        }


        public void updateCloudFL()
        {
            cloudFL.position.X -= cloudFL.speed;

            if (cloudFL.position.X < -fenetre.Width)
            {
                cloudFL.position.X = fenetre.Width;
            }
        }

        public void updateCloudFR()
        {
            cloudFR.position.X -= cloudFR.speed;

            if (cloudFR.position.X < -fenetre.Width)
            {
                cloudFR.position.X = fenetre.Width;
            }
        }

        public void updateCage()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (cage.height < (600 / cage.speed))
                {
                    cage.position.Y += cage.speed;
                    cage.height++;
                    gear.objetState = Mechanism.etats.moving;
                }
                else
                    cage.position.Y += 0;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (cage.height > 0)
                {
                    cage.position.Y -= cage.speed;
                    cage.height--;
                    gear.objetState = Mechanism.etats.moving;

                }
                else
                    cage.position.Y += 0;
            }
        }

        public void updateJuan()
        {
            //Juan suit la position de la cage.
            juan.position.X = (int)cage.position.X - 10;
            juan.position.Y = (int)cage.position.Y + 40;

            if (juan.objetState == Player.etats.idle)
            {
                juan.spriteAfficher = juan.tabIdle[juan.idleState];
            }
            if (juan.objetState == Player.etats.shoot)
            {
                juan.spriteAfficher = juan.tabShoot[juan.shootState];
            }
            //juan.cpt ++ => Compteur permettant de gérer le changement d'images
            juan.cpt++;
            if (juan.cpt == 5) //Temps d'attente avant de changer de frame.   1=rapide 10=lent
            {
                //Gestion des animations. Si le compteur (cpt) == le chiffre mentionné, on change de frame.
                juan.shootState++;
                juan.idleState++;

                //Si la dernière frame est atteinte, on recommence à la première.
                if (juan.shootState == juan.nbEtatShoot)
                {
                    juan.shootState = 0;
                }
                if (juan.idleState ==  juan.nbEtatIdle)
                {
                    juan.idleState = 0;
                }
                juan.cpt = 0;
            }
        }

        public void updateMechanism()
        {
            if (gear.objetState == Mechanism.etats.moving)
            {
                gear.spriteAfficher = gear.tabGear[gear.state];
                pulley.spriteAfficher = pulley.tabPulley[pulley.state];
            }

            gear.cpt++;
            pulley.cpt++;

            if (gear.cpt == 5) //Temps d'attente avant de changer de frame.   1=rapide 10=lent
            {
                //Gestion des animations. Si le compteur (cpt) == le chiffre mentionné, on change de frame.
                if (Keyboard.GetState().IsKeyDown(Keys.W) && cage.height > 0)
                {
                    gear.state++;
                    pulley.state++;

                    if (gear.state == gear.nbEtatDown)
                    {
                        gear.state = 0;
                        pulley.state = 0;
                    }

                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) && cage.height<(600/cage.speed))
                {
                    if (gear.state == 0)
                    {
                        gear.state = gear.nbEtatUp;
                        pulley.state = pulley.nbEtatUp;
                    }

                    gear.state--;
                    pulley.state--;


                }

                gear.cpt = 0;
            }
        }

        private void DrawRope(Vector2 p1, Vector2 p2)
        {
            float angle = (float)Math.Atan2(p1.Y - p2.Y, p1.X - p2.X);
            float dist = Vector2.Distance(p1, p2);

            spriteBatch.Draw(rope, new Rectangle((int)p2.X, (int)p2.Y, (int)dist, 2), null, Color.BurlyWood, angle, Vector2.Zero, SpriteEffects.None, 0);

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
            spriteBatch.Draw(wallpaper.sprite, wallpaper.position);

            //Affichage du cloudB(ack)
            spriteBatch.Draw(cloudBL.sprite, cloudBL.position);
            spriteBatch.Draw(cloudBR.sprite, cloudBR.position);

            //Affichage du temple
            spriteBatch.Draw(temple.sprite, temple.position);

            //Affichage de la corde #1
            DrawRope(new Vector2(gear.position.X + 50, 338), new Vector2(pulley.position.X + 62, pulley.position.Y + 6));

            //Affichage de la gear
            spriteBatch.Draw(gear.sprite, gear.position, gear.spriteAfficher, Color.White);

            //Affichage de la cage
            spriteBatch.Draw(cage.sprite, cage.position);

            //Affichage de la corde #2
            DrawRope(new Vector2(cage.position.X + 46, 214), new Vector2(cage.position.X + 46, cage.position.Y + 6));
            

            //Affichage de la pulley
            spriteBatch.Draw(pulley.sprite, pulley.position, pulley.spriteAfficher, Color.White);

            //Affichage de Juan
            spriteBatch.Draw(juan.sprite, juan.position, juan.spriteAfficher, Color.White);

            //Affichage du cloudM(iddle)
            spriteBatch.Draw(cloudML.sprite, cloudML.position);
            spriteBatch.Draw(cloudMR.sprite, cloudMR.position);

            //Affichage du cloudF(ront)
            spriteBatch.Draw(cloudFL.sprite, cloudFL.position);
            spriteBatch.Draw(cloudFR.sprite, cloudFR.position);

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
