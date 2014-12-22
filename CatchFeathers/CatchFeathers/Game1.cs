using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Feather_around_you.sprite;
using Feather_around_you.Conponent;

namespace Feather_around_you.WindowsGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteManage spriteManage;
        userInterface UI;
        GameState currentState = GameState.Start;

        /// <summary>
        /// The backgrounds of game
        /// </summary>
        Texture2D Start;
        Texture2D BackGround;
        Texture2D End;
        Texture2D TimeOverBackGround;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            spriteManage = new SpriteManage(this);
            UI = new userInterface(this, spriteManage, spriteBatch);
            rnd = new Random();
            
            
            graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.WorkingArea.Width;
            graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.WorkingArea.Height;
            graphics.IsFullScreen = true;   
        }

        public Random rnd { get; protected set; }

        public GameState gameState
        {
            get { return currentState; }
            set { currentState = value; }
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
            Components.Add(UI);
            Components.Add(spriteManage);
            
            spriteManage.Enabled = false;
            spriteManage.Visible = false;
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

            Start = Content.Load<Texture2D>(@"material\Start");
            BackGround = Content.Load<Texture2D>(@"material\AIR");
            End = Content.Load<Texture2D>(@"material\End");
            TimeOverBackGround = Content.Load<Texture2D>(@"material\TimeOverBackGround");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.Window.AllowUserResizing = true;

            switch (currentState)
            {
                case GameState.Start:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Begin();
                    spriteBatch.Draw(Start, Vector2.Zero, null, Color.White, 0, Vector2.Zero
                        , (float)this.Window.ClientBounds.Width / (float)Start.Width + 0.1f, SpriteEffects.None, 1);
                    spriteBatch.End();
                    
                    break;
                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(BackGround, Vector2.Zero, null, Color.White,
                        0, Vector2.Zero, (float)this.Window.ClientBounds.Width / (float)BackGround.Width + 0.1f,
                        SpriteEffects.None, 0);
                    spriteBatch.End();
                    break;
                case GameState.Resume:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(TimeOverBackGround, Vector2.Zero, null, Color.White, 0, Vector2.Zero
                        , (float)this.Window.ClientBounds.Width / (float)TimeOverBackGround.Width + 0.1f, 
                        SpriteEffects.None, 0);
                    spriteBatch.End();

                    break;
                case GameState.Over:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(End, Vector2.Zero, null, Color.White, 0, Vector2.Zero
                        , (float)this.Window.ClientBounds.Width / (float)BackGround.Width + 1, SpriteEffects.None, 0);
                    spriteBatch.End();

                    break;
                case GameState.TimeOver:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(TimeOverBackGround, Vector2.Zero, null, Color.White, 0, Vector2.Zero
                        , (float)this.Window.ClientBounds.Width / (float)BackGround.Width + 1, SpriteEffects.None, 0);
                    spriteBatch.End();

                    break;
            }
            base.Draw(gameTime);
        }
    }
}
