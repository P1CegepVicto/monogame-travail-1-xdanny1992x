using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using Microsoft.Xna.Framework.Media;
using Game1;

namespace Projet_03
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Rectangle fenetre;
        GameObject wallpaper;
        GameObject menu;
        GameObject GO;
        GameObject choix;
        GameObject temple;
        GameObject cloudBL;
        GameObject cloudBR;
        GameObject cloudML;
        GameObject cloudMR;
        GameObject cloudFL;
        GameObject cloudFR;
        GameObject cage;
        GameObject[] HUD = new GameObject[3];
        GameObject[] bullet = new GameObject[5];
        Player juan;
        Mechanism gear;
        Mechanism pulley;
        Enemy[] fly = new Enemy[300];
        Random de = new Random();
        float newFly = 0;
        float sinceLastBullet = 0;
        float sinceLastLive = 0;
        int currentFly = 0;
        int currentBullet = 0;
        float totalGameTime;
        bool counting =false;
        bool repeat = true;
        KeyboardState previousKey;
        Texture2D rope;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum state { MainMenu, Play, GameOver };
        state objetState = state.MainMenu;
        //SOUNDS
        SoundEffect son;
        SoundEffectInstance gearS;
        SoundEffect son1;
        SoundEffectInstance whoopS;
        SoundEffect son2;
        SoundEffectInstance MainMenuS;
        SoundEffect son3;
        SoundEffectInstance GameOverS;
        SoundEffect son4;
        SoundEffectInstance PlayingS;
        SoundEffect son5;
        SoundEffectInstance DieS;
        SoundEffect son6;
        SoundEffectInstance LaughS;
        //FONT
        SpriteFont font;
        string GameOverF = "";



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
            juan.live = 3;
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

            //Fly
            for (int i = 0; i < fly.Length; i++)
            {
                fly[i] = new Enemy();
                fly[i].objetState = Enemy.etats.flying;
                fly[i].sprite = Content.Load<Texture2D>("Bee_SpriteSheet.png");
                fly[i].canKill = true;
                fly[i].position = new Rectangle(1930, -8000, 64, 44);
                fly[i].spriteAfficher = fly[i].tabFlying[1];
                fly[i].speed = de.Next(50000, 350000);
                fly[i].isAlive = false;
                fly[i].fall = 0;
            }

            //Bullet
            for (int i = 0; i < bullet.Length; i++)
            {
                bullet[i] = new GameObject();
                bullet[i].isAlive = false;
                bullet[i].sprite = Content.Load<Texture2D>("Bullet.png");
                bullet[i].speed = 12;
                bullet[i].position.X = juan.position.X + 64;
                bullet[i].position.Y = juan.position.Y + 48;
            }

            //MainMenu
            menu = new GameObject();
            menu.sprite = Content.Load<Texture2D>("MainMenu.png");
            menu.position.X = fenetre.X;
            menu.position.Y = fenetre.Y;
            objetState = state.MainMenu;

            //MainMenu
            GO = new GameObject();
            GO.sprite = Content.Load<Texture2D>("GameOver.png");
            GO.position.X = fenetre.X;
            GO.position.Y = fenetre.Y;

            //Choix
            choix = new GameObject();
            choix.sprite = Content.Load<Texture2D>("choix.png");
            choix.position.X = 1200;
            choix.position.Y = 685;
            objetState = state.MainMenu;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //ROPE
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rope = Content.Load<Texture2D>("Rope.png");

            //SOUNDS
            son = Content.Load<SoundEffect>("Sounds\\Gear");
            gearS = son.CreateInstance();
            son1 = Content.Load<SoundEffect>("Sounds\\Whoop");
            whoopS = son1.CreateInstance();
            son2 = Content.Load<SoundEffect>("Sounds\\MainMenu");
            MainMenuS = son2.CreateInstance();
            son3 = Content.Load<SoundEffect>("Sounds\\GameOver");
            GameOverS = son3.CreateInstance();
            son4 = Content.Load<SoundEffect>("Sounds\\Playing");
            PlayingS = son4.CreateInstance();
            son5 = Content.Load<SoundEffect>("Sounds\\Die");
            DieS = son5.CreateInstance();
            son6 = Content.Load<SoundEffect>("Sounds\\Laugh");
            LaughS = son6.CreateInstance();

            //FONT
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
            //MAIN MENU
            if (objetState == state.MainMenu)
            {
                MainMenuS.Play();
                if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    choix.position.Y = 790;
                    choix.position.X = 1300;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    choix.position.Y = 685;
                    choix.position.X = 1200;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (choix.position.Y == 685)
                    {
                        objetState = state.Play;
                        counting = true;
                        MainMenuS.Stop();
                    }
                    if (choix.position.Y == 790)
                        Exit();
                }
            }

            //GAME OVER
            if (objetState == state.GameOver )
            {
                choix.position.X = 1300;
                GameOverF = Convert.ToString(totalGameTime);
                counting = false;
                if (repeat == true)
                {
                    PlayingS.Stop();
                    GameOverS.Play();
                    repeat = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    choix.position.Y = 790;
                    choix.position.X = 1300;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up)|| Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    choix.position.X = 1300;
                    choix.position.Y = 685;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (choix.position.Y == 685)
                    {
                        for (int i = 0; i < fly.Length; i++)
                        {
                            if (objetState == state.GameOver)
                            {
                                fly[i].isAlive = false;
                                fly[i].position.X = 9999;
                                cage.position.Y = 256;
                                cage.height = 0;
                                totalGameTime = 0;
                                counting = true;
                                objetState = state.Play;
                            }
                        }
                        objetState = state.Play;
                        juan.live = 3;
                        
                    }
                    if (choix.position.Y == 790)
                        Exit();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                juan.objetState = Player.etats.shoot;
            }
            else
            {
                juan.objetState = Player.etats.idle;
            }

            if (objetState == state.Play)
            {
            PlayingS.Play();   
            updateCloudBL();
            updateCloudBR();
            updateCloudML();
            updateCloudMR();
            updateCloudFL();
            updateCloudFR();
            updateCage();
            updateJuan();
            updateMechanism();
            updateEnemy();
            updateBullet();
            }


            //Création des mouches
            if (objetState == state.Play)
            {
                newFly += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (newFly >= (float)de.Next(2, 7))//Création d'une mouche à toutes les [random X] secondes.
                {
                    if (fly[currentFly].isAlive == false)
                    {
                        fly[currentFly].position = new Rectangle(1930, de.Next(320, fenetre.Height - 186), 64, 44);
                        fly[currentFly].isAlive = true;
                        newFly = 0;
                        currentFly += 1;
                    }

                }

                if (currentFly > fly.Length - 1)
                    currentFly = 0;
            }
            //Création des projectiles
            if (objetState == state.Play)
            {
                sinceLastBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (juan.spriteAfficher == juan.tabShoot[4] && (Keyboard.GetState().IsKeyUp(Keys.D)))
                {
                    juan.spriteAfficher = juan.tabShoot[4];
                    bullet[currentBullet].isAlive = true;
                    currentBullet++;
                    whoopS.Play();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (sinceLastBullet >= 1f / 2)
                    {
                        bullet[currentBullet].isAlive = true;
                        currentBullet++;
                        sinceLastBullet = 0;
                        whoopS.Play();
                    }
                }

                if (currentBullet > bullet.Length - 1)
                {
                    currentBullet = 0;
                }
            }

            if (objetState == state.Play)
            {
                //Intervalle entre les vies perdues
                sinceLastLive += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Temps total d'une partie
                if (counting==true)
                {
                    totalGameTime += (float)gameTime.TotalGameTime.TotalSeconds/340;
                }

                previousKey = Keyboard.GetState();

                base.Update(gameTime);
            }
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

            if (Keyboard.GetState().IsKeyDown(Keys.S)|| Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (cage.height < (600 / cage.speed))
                {
                    cage.position.Y += cage.speed;
                    cage.height++;
                    gear.objetState = Mechanism.etats.moving;
                    gearS.Play();
                }
                else
                    cage.position.Y += 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W)|| Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (cage.height > 0)
                {
                    cage.position.Y -= cage.speed;
                    cage.height--;
                    gear.objetState = Mechanism.etats.moving;
                    gearS.Play();

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
                if (juan.idleState == juan.nbEtatIdle)
                {
                    juan.idleState = 0;
                }
                juan.cpt = 0;
            }
            if (juan.live < 1)
            {
                objetState = state.GameOver;
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
                if ((Keyboard.GetState().IsKeyDown(Keys.W)|| Keyboard.GetState().IsKeyDown(Keys.Up)) && cage.height > 0)
                {
                    gear.state++;
                    pulley.state++;

                    if (gear.state == gear.nbEtatDown)
                    {
                        gear.state = 0;
                        pulley.state = 0;
                    }

                }
                if ((Keyboard.GetState().IsKeyDown(Keys.S)|| Keyboard.GetState().IsKeyDown(Keys.Down)) && cage.height < (600 / cage.speed))
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
        public void updateEnemy()
        {

            for (int i = 0; i < fly.Length; i++)
            {
                if (fly[i].isAlive == true)
                {
                    fly[currentFly].position = new Rectangle(1930, de.Next(320, fenetre.Height - 186), 64, 44);
                    fly[i].position.X -= (int)(fly[i].speed) / 100000 *2; //Vitesse
                }
                if (fly[i].isDead == true)
                {
                    fly[i].position.Y += (int)(fly[i].fall) / 2;
                    fly[i].fall += (float)0.6;
                }

                fly[i].cpt++;

                if (fly[i].objetState == Enemy.etats.flying)
                {
                    fly[i].spriteAfficher = fly[i].tabFlying[fly[i].state];
                }
                else if (fly[i].objetState == Enemy.etats.dead)
                {
                    fly[i].spriteAfficher = fly[i].tabDead[fly[i].state];
                }

                if (fly[i].cpt == 5) //Temps d'attente avant de changer de frame.   1=rapide 10=lent
                {
                    fly[i].state++;

                    if (fly[i].state == fly[i].nbEtatFlying)
                    {
                        fly[i].state = 0;
                    }
                    fly[i].cpt = 0;
                }
                if (fly[i].position.X + fly[i].sprite.Width < fenetre.X || fly[i].position.Y - fly[i].sprite.Height > fenetre.Height)
                {
                    fly[i].isAlive = false;
                    fly[i].isDead = true;
                }
                if (fly[i].position.X + fly[i].sprite.Width < fenetre.X && fly[i].canKill == true)
                {
                    if (sinceLastLive > 1f)
                    {
                        juan.live--;
                        fly[i].position.X = 9999;
                        sinceLastLive = 0;
                        LaughS.Play();
                    }
                }

                for (int j = 0; j < bullet.Length; j++)
                {
                    if (fly[i].isAlive == true && fly[i].GetRect().Intersects(bullet[j].GetRect())&&bullet[j].isAlive==true)
                    {
                        bullet[j].isAlive = false;
                        fly[i].isDead = true;
                        fly[i].canKill = false;
                        DieS.Play();
                        fly[i].objetState = Enemy.etats.dead;
                    }
                }

            }

        }

        public void updateBullet()
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].isAlive == true)
                {
                    bullet[i].position.X += bullet[i].speed;
                    
                }
                else
                {
                    bullet[i].position.X = juan.position.X + 88;
                    bullet[i].position.Y = juan.position.Y + 52;
                }

                if (bullet[i].position.X > fenetre.Width)
                {
                    bullet[i].isAlive = false;
                }
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

            //Affichage des projectiles
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].isAlive == true)
                    spriteBatch.Draw(bullet[i].sprite, bullet[i].position, Color.White);
            }

            //Affichage de Juan
            spriteBatch.Draw(juan.sprite, juan.position, juan.spriteAfficher, Color.White);

            //Affichage des mouches
            for (int i = 0; i < fly.Length; i++)
            {
                spriteBatch.Draw(fly[i].sprite, fly[i].position, fly[i].spriteAfficher, Color.White);
            }

            //Affichage du cloudM(iddle)
            spriteBatch.Draw(cloudML.sprite, cloudML.position);
            spriteBatch.Draw(cloudMR.sprite, cloudMR.position);

            //Affichage du cloudF(ront)
            spriteBatch.Draw(cloudFL.sprite, cloudFL.position);
            spriteBatch.Draw(cloudFR.sprite, cloudFR.position);
                        
            //Affichage du MainMenu
            if (objetState == state.MainMenu)
                spriteBatch.Draw(menu.sprite, menu.position);

            //Affichage du GameOver
            if (objetState == state.GameOver)
            {
                spriteBatch.Draw(GO.sprite, GO.position);
            }

            //Affichage du Choix
            if (objetState == state.MainMenu || objetState == state.GameOver)
                spriteBatch.Draw(choix.sprite, choix.position);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
