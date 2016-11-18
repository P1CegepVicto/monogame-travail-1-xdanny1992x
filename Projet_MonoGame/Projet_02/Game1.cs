using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        GameObject tortue;
        GameObject[] pipeB = new GameObject[2];
        GameObject[] pipeT = new GameObject[2];
        GameObject wallpaper;

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
            for (int i = 0; i <=1; i++)
            {
                pipeB[i] = new GameObject();
                pipeB[i].sprite = Content.Load<Texture2D>("sBottom.png");
                pipeB[i].position.Y = (fenetre.Height / 2) - (pipeB[i].sprite.Height / 2);
            if (i == 0)
                {
                    pipeB[i].position.X = fenetre.X-86;
                }
                else if(i ==1)
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
            cochon.position.X = fenetre.Width / 2;
            cochon.position.Y = fenetre.Height / 2;
            cochon.origin.X = cochon.GetRect().Width / 2;
            cochon.origin.Y = cochon.GetRect().Height / 2;
            //////////////////////////////////////////////////////////////////////
            //Ma ligne de code qui ne fonctionne pas
            //cochon.origin = (cochon.GetRect().Width/2, cochon.GetRect().Height/2)
            //////////////////////////////////////////////////////////////////////
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
                cochon.rotation -= 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                cochon.rotation += 0.1f;

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
            spriteBatch.Draw(wallpaper.sprite, wallpaper.position, Color.White);
            for (int i = 0; i <= 1; i++)
            {
                if (pipeB[i].position.X < (fenetre.Width / 2))
                { 
                spriteBatch.Draw(pipeB[i].sprite, pipeB[i].position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    spriteBatch.Draw(pipeT[i].sprite, pipeT[i].position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spriteBatch.Draw(pipeB[i].sprite, pipeB[i].position, Color.White);
                    spriteBatch.Draw(pipeT[i].sprite, pipeT[i].position, Color.White);
                }
            }
            spriteBatch.Draw(cochon.sprite, cochon.position,null, Color.White, cochon.rotation, cochon.origin, 1f, SpriteEffects.None, 0);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
