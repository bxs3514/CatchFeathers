using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Feather_around_you.WindowsGame;
using Feather_around_you.sprite;

namespace Feather_around_you.Conponent
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    /// 

    public enum GameState
    { Start, InGame, Resume, Over, TimeOver};
    public struct Time
    {
        public int hour;
        public int minute;
        public int second;
    }

    public class userInterface : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont StartFont;
        SpriteFont OverFont;
        SpriteFont gameTimeFont;
        SpriteFont TimeOverFont;
        SpriteFont HelpFont;

        SpriteManage spriteManage;

        Song StartMusic;
        Song InMusic;
        Song OverMusic;

        Texture2D Title;
        Texture2D NewGame1;
        Texture2D NewGame2;
        Texture2D Help1;
        Texture2D Help2;
        Texture2D Exit0;
        Texture2D Exit1;
        Texture2D Exit2;
        Texture2D Retry;
        Texture2D BackToMenu;
        Texture2D Continue;
        Texture2D TimeOverBack;
        Texture2D Back;

        Time currentTime;
        Time showTime;

        string OverString = "Game Over";
        string TimeOverString = "Time Over";

        private bool show;

        public userInterface(Game game, SpriteManage spriteManage, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.spriteManage = spriteManage;
            currentTime = new Time();
            showTime = new Time();
            currentTime.hour = currentTime.minute = currentTime.second = 0;
            showTime = currentTime;
            show = false;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            StartMusic = Game.Content.Load<Song>(@"Audio\Start");
            InMusic = Game.Content.Load<Song>(@"Audio\In");
            OverMusic = Game.Content.Load<Song>(@"Audio\Over");
            Title = Game.Content.Load<Texture2D>(@"Interface\Title");
            NewGame1 = Game.Content.Load<Texture2D>(@"Interface\New Game1");
            NewGame2 = Game.Content.Load<Texture2D>(@"Interface\New Game2");
            Help1 = Game.Content.Load<Texture2D>(@"material\Help1");
            Help2 = Game.Content.Load<Texture2D>(@"material\Help2");
            Exit0 = Game.Content.Load<Texture2D>(@"Interface\Exit");
            Exit1 = Game.Content.Load<Texture2D>(@"Interface\Exit1");
            Exit2 = Game.Content.Load<Texture2D>(@"Interface\Exit2");
            Retry = Game.Content.Load<Texture2D>(@"Interface\retry");
            BackToMenu = Game.Content.Load<Texture2D>(@"Interface\Back");
            Back = Game.Content.Load<Texture2D>(@"Interface\Back2");
            Continue = Game.Content.Load<Texture2D>(@"Interface\continue");
            StartFont = Game.Content.Load<SpriteFont>(@"Font\StartMenu");
            OverFont = Game.Content.Load<SpriteFont>(@"Font\Over");
            TimeOverFont = Game.Content.Load<SpriteFont>(@"Font\TimeOver");
            gameTimeFont = Game.Content.Load<SpriteFont>(@"Font\gameTime");
            HelpFont = Game.Content.Load<SpriteFont>(@"Font\HelpFont");

            MediaPlayer.Play(StartMusic);
            base.LoadContent();
        }
        /// <summary>
        /// Control the time of the game
        /// </summary>
        public Time TimeControl(GameTime gameTime, Time currentTime)
        {
            Time finalTime = new Time();
            if (gameTime.TotalGameTime.Seconds < currentTime.second)
            {
                currentTime.minute++;
                finalTime.second = Math.Abs(60 + gameTime.TotalGameTime.Seconds - currentTime.second);
            }
            else
                finalTime.second = Math.Abs(gameTime.TotalGameTime.Seconds - currentTime.second);
            if (gameTime.TotalGameTime.Minutes < currentTime.minute)
            {
                currentTime.hour++;
                finalTime.minute = Math.Abs(60 + gameTime.TotalGameTime.Minutes - currentTime.minute);
            }

            return finalTime;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            showTime = TimeControl(gameTime, currentTime);

            switch (((Game1)Game).gameState)
            {
                // TODO: Add your update logic here
                case GameState.Start:
                    currentTime.hour = gameTime.TotalGameTime.Hours;
                    currentTime.minute = gameTime.TotalGameTime.Minutes;
                    currentTime.second = gameTime.TotalGameTime.Seconds;

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed
                        && (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 5 * 4
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 5 * 4
                        + NewGame1.Width) && (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 3 + 30 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 3 + NewGame1.Height + 30))
                    {
                        ((Game1)Game).gameState = GameState.InGame;
                        spriteManage.Enabled = true;
                        spriteManage.Visible = true;
                        MediaPlayer.Play(InMusic);
                    }
                    else if (Mouse.GetState().LeftButton == ButtonState.Pressed && 
                        (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 5 * 4
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 5 * 4
                        + NewGame1.Width) && (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 - 30 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 - 30 + Help1.Height))
                    {
                        show = true;
                    }
                    else if (Mouse.GetState().LeftButton == ButtonState.Pressed
                        && (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 5 * 4
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 5 * 4
                        + NewGame1.Width) && (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 + 30 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + NewGame1.Height + 30))
                    {
                        Game.Exit();
                    }
                    break;
                case GameState.InGame:

                    if ((Mouse.GetState().LeftButton == ButtonState.Pressed
                        && (Mouse.GetState().X >= 20 && Mouse.GetState().X <= 20 + Back.Width) &&
                        (Mouse.GetState().Y >= Game.Window.ClientBounds.Height - 20 - Back.Height &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height - 20)) ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape)) 
                    {
                        ((Game1)Game).gameState = GameState.Resume;

                        spriteManage.Enabled = false;
                        spriteManage.Visible = false;
                    }
                    Console.WriteLine("{0},{1}", gameTime.TotalGameTime.Minutes, currentTime.minute);
                    Console.WriteLine("{0} ", showTime.second);
                    if (showTime.second == 0 && gameTime.TotalGameTime.Minutes != currentTime.minute)
                    {
                        ((Game1)Game).gameState = GameState.TimeOver;
                        spriteManage.Enabled = false;
                        spriteManage.Visible = false;
                        MediaPlayer.Play(OverMusic);
                    }
                    if (spriteManage.GetLife <= 0)
                    {
                        ((Game1)Game).gameState = GameState.Over;
                        spriteManage.Enabled = false;
                        spriteManage.Visible = false;
                        MediaPlayer.Play(OverMusic);
                    }
                    
                    break;
                case GameState.Resume:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 2 - Retry.Width / 2
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 2 + Retry.Width / 2) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 - 10 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + Retry.Height - 10))
                    {
                        show = false;
                        ((Game1)Game).gameState = GameState.InGame;

                        spriteManage.Enabled = true;
                        spriteManage.Visible = true;
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 2 - Retry.Width / 2
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 2 + Retry.Width / 2) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 + 50 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + Retry.Height + 50))
                    {
                        show = false;
                        ((Game1)Game).gameState = GameState.InGame;

                        currentTime.hour = gameTime.TotalGameTime.Hours;
                        currentTime.minute = gameTime.TotalGameTime.Minutes;
                        currentTime.second = gameTime.TotalGameTime.Seconds;

                        spriteManage.GetLife = 5;
                        spriteManage.GetScore = 0;
                        spriteManage.GetLevel = 0;

                        spriteManage.Enabled = true;
                        spriteManage.Visible = true;
                        MediaPlayer.Play(InMusic);
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 2 - BackToMenu.Width / 2
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 2 + BackToMenu.Width / 2) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 + 110 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + BackToMenu.Height + 110))
                    {
                        show = false;
                        ((Game1)Game).gameState = GameState.Start;

                        spriteManage.GetLife = 5;
                        spriteManage.GetScore = 0;
                        spriteManage.GetLevel = 0;
                        spriteManage.Enabled = false;
                        spriteManage.Visible = false;
                        MediaPlayer.Play(StartMusic);
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width - Exit0.Width &&
                         Mouse.GetState().X <= Game.Window.ClientBounds.Width) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height - Exit0.Height &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height))
                        Game.Exit();
                    break;
                case GameState.Over:
                case GameState.TimeOver:
                    if ((Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 2 - Retry.Width / 2
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 2 + Retry.Width / 2) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 + 50 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + Retry.Height + 50))
                        || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        ((Game1)Game).gameState = GameState.InGame;

                        currentTime.hour = gameTime.TotalGameTime.Hours;
                        currentTime.minute = gameTime.TotalGameTime.Minutes;
                        currentTime.second = gameTime.TotalGameTime.Seconds;

                        spriteManage.GetLife = 5;
                        spriteManage.GetScore = 0;
                        spriteManage.GetLevel = 0;
                        spriteManage.Enabled = true;
                        spriteManage.Visible = true;
                        MediaPlayer.Play(InMusic);
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width / 2 - BackToMenu.Width / 2
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 2 + BackToMenu.Width / 2) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 + 110 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + BackToMenu.Height + 110))
                    {
                        show = false;
                        ((Game1)Game).gameState = GameState.Start;

                        spriteManage.GetLife = 5;
                        spriteManage.GetScore = 0;
                        spriteManage.GetLevel = 0;
                        spriteManage.Enabled = false;
                        spriteManage.Visible = false;
                        MediaPlayer.Play(StartMusic);
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                       (Mouse.GetState().X >= Game.Window.ClientBounds.Width - Exit0.Width &&
                         Mouse.GetState().X <= Game.Window.ClientBounds.Width) &&
                       (Mouse.GetState().Y >= Game.Window.ClientBounds.Height - Exit0.Height &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height))
                        Game.Exit();
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            switch (((Game1)Game).gameState)
            {
                case GameState.Start:
                    Game.IsMouseVisible = true;

                    spriteBatch.Begin();
                    
                    spriteBatch.Draw(Title, new Vector2(Game.Window.ClientBounds.Width / 2 - Title.Bounds.Width / 2,
                        Game.Window.ClientBounds.Width / 7), null, Color.White,
                        0, Vector2.Zero, 1, SpriteEffects.None, 0);

                    if(show == true)
                        spriteBatch.DrawString(HelpFont, "You should try to catch the purple feather the" +
                            "the pink feather and the white feather.\nEspecially,the white feather will escape" +
                            "if you try to approach them!\nBut you can get 300 score if you catch them skillfully!\n" +
                            "By the way,you only can get 100 score if you catch the other feathers.\n" +
                            "Your level will up when your score is enough high and of course it will be more\n" +
                            "difficult to survive if your level is high." + "And if you"
                            + "catched by the red bumb your life \nwill reduce,of course you can add life if you catch"
                            + "the needle.\n" + "When your life come to zero or the time is zero.The game is finished,you\n"
                            + "will get your finally score!Good luck!",
                            new Vector2(Game.Window.ClientBounds.Width / 6,
                            Game.Window.ClientBounds.Height / 5 * 3), Color.Black, 0, Vector2.Zero, 1,
                            SpriteEffects.None, 1);

                    if ((Mouse.GetState().X >= Game.Window.ClientBounds.Width / 5 * 4
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 5 * 4
                        + NewGame1.Width) && (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 3 + 30 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 3 + 30 + NewGame1.Height))
                    {
                        spriteBatch.Draw(NewGame2, new Vector2(Game.Window.ClientBounds.Width / 5 * 4,
                        Game.Window.ClientBounds.Height / 3 + 30), Color.White);
                    }
                    else
                        spriteBatch.Draw(NewGame1, new Vector2(Game.Window.ClientBounds.Width / 5 * 4,
                        Game.Window.ClientBounds.Height / 3 + 30), Color.White);

                    if ((Mouse.GetState().X >= Game.Window.ClientBounds.Width / 5 * 4
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 5 * 4
                        + NewGame1.Width) && (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 - 
                        Game.Window.ClientBounds.Height / 20 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 -
                        Game.Window.ClientBounds.Height / 20 + Help1.Height))
                    {
                        spriteBatch.Draw(Help2, new Vector2(Game.Window.ClientBounds.Width / 5 * 4,
                        Game.Window.ClientBounds.Height / 2 - Game.Window.ClientBounds.Height / 20), Color.White);
                    }
                    else
                        spriteBatch.Draw(Help1, new Vector2(Game.Window.ClientBounds.Width / 5 * 4,
                        Game.Window.ClientBounds.Height / 2 - Game.Window.ClientBounds.Height / 20), Color.White);

                    if ((Mouse.GetState().X >= Game.Window.ClientBounds.Width / 5 * 4
                        && Mouse.GetState().X <= Game.Window.ClientBounds.Width / 5 * 4
                        + NewGame1.Width) && (Mouse.GetState().Y >= Game.Window.ClientBounds.Height / 2 + 30 &&
                        Mouse.GetState().Y <= Game.Window.ClientBounds.Height / 2 + NewGame1.Height + 30))
                    {
                        spriteBatch.Draw(Exit2, new Vector2(Game.Window.ClientBounds.Width / 5 * 4,
                            Game.Window.ClientBounds.Height / 2 + 30), Color.White);
                    }
                    else
                        spriteBatch.Draw(Exit1, new Vector2(Game.Window.ClientBounds.Width / 5 * 4,
                            Game.Window.ClientBounds.Height / 2 + 30), Color.White);

                    spriteBatch.DrawString(StartFont, "Welcome to the world" +
                    "full of feathers! \nTry catch as more as feathers and \navoid to "
                    + "be catched by the torch!!!\n", new Vector2(Game.Window.ClientBounds.Width / 6, 
                        Game.Window.ClientBounds.Height / 5 * 2), Color.Black, 0, Vector2.Zero, 1,
                    SpriteEffects.None, 1);

                    spriteBatch.End();

                    break;
                case GameState.InGame:
                    Game.IsMouseVisible = false;

                    spriteBatch.Begin();
                    if(showTime.minute == 0 && showTime.second == 0)
                        spriteBatch.DrawString(gameTimeFont, "Time: 1:00" ,
                            new Vector2(Game.Window.ClientBounds.Width - 120, 10), Color.Red);
                    else if (showTime.second <= 50)
                        spriteBatch.DrawString(gameTimeFont, "Time: " +  showTime.minute + ":"
                             + (60 - showTime.second), 
                            new Vector2(Game.Window.ClientBounds.Width - 120, 10), Color.Red);
                    else
                        if (showTime.second != 0)
                            spriteBatch.DrawString(gameTimeFont, "Time: " + showTime.minute 
                             + ":0" + (60 - showTime.second), new Vector2(Game.Window.ClientBounds.Width - 120, 10), 
                             Color.Red);
                        else
                            spriteBatch.DrawString(gameTimeFont, "Time: " + showTime.minute + ":"
                                + "59", new Vector2(Game.Window.ClientBounds.Width - 120, 0), Color.Red);

                    spriteBatch.Draw(Back, new Vector2(20, Game.Window.ClientBounds.Height - Back.Height - 20), Color.White);

                    spriteBatch.End();

                    break;
                case GameState.Resume:
                    Game.IsMouseVisible = true;

                    spriteBatch.Begin();

                    spriteBatch.Draw(Continue, new Vector2(Game.Window.ClientBounds.Width / 2 - Continue.Width / 2,
                        Game.Window.ClientBounds.Height / 2 - 10), Color.White);
                    spriteBatch.Draw(Retry, new Vector2(Game.Window.ClientBounds.Width / 2 - Retry.Width / 2,
                        Game.Window.ClientBounds.Height / 2 + 50), Color.White);
                    spriteBatch.Draw(BackToMenu, new Vector2(Game.Window.ClientBounds.Width / 2 - BackToMenu.Width / 2,
                        Game.Window.ClientBounds.Height / 2 + 110), Color.White);
                    spriteBatch.End();

                    break;
                case GameState.Over:
                    Game.IsMouseVisible = true;

                    spriteBatch.Begin();
                    spriteBatch.DrawString(OverFont, "      " + OverString + "\n\n   Your score: "
                        + spriteManage.GetScore + "\n\n Try harder next time",
                        new Vector2(Game.Window.ClientBounds.Width / 2 -
                            OverFont.MeasureString(OverString).X - 30,
                            Game.Window.ClientBounds.Height / 2 -
                            OverFont.MeasureString(OverString).Y - 70),
                            Color.Orange, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    spriteBatch.Draw(Retry, new Vector2(Game.Window.ClientBounds.Width / 2 - Retry.Width / 2,
                        Game.Window.ClientBounds.Height / 2 + 50), Color.White);
                    spriteBatch.Draw(BackToMenu, new Vector2(Game.Window.ClientBounds.Width / 2 - Retry.Width / 2,
                        Game.Window.ClientBounds.Height / 2 + 110), Color.White);
                    spriteBatch.Draw(Exit0, new Vector2(Game.Window.ClientBounds.Width - Exit0.Width,
                        Game.Window.ClientBounds.Height - Exit0.Height), Color.White);
                    spriteBatch.End();

                    break;
                case GameState.TimeOver:
                    Game.IsMouseVisible = true;

                    spriteBatch.Begin();
                    spriteBatch.DrawString(TimeOverFont, "      " + TimeOverString + "\n\n   Your score: "
                        + spriteManage.GetScore + "\n\n Try harder next time",
                        new Vector2(Game.Window.ClientBounds.Width / 2 -
                            OverFont.MeasureString(OverString).X - 30,
                            Game.Window.ClientBounds.Height / 2 -
                            OverFont.MeasureString(OverString).Y - 70),
                            Color.Orange, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    spriteBatch.Draw(Retry, new Vector2(Game.Window.ClientBounds.Width / 2 - Retry.Width / 2,
                        Game.Window.ClientBounds.Height / 2 + 50), Color.White);
                    spriteBatch.Draw(BackToMenu, new Vector2(Game.Window.ClientBounds.Width / 2 - Retry.Width / 2,
                        Game.Window.ClientBounds.Height / 2 + 110), Color.White);
                    spriteBatch.Draw(Exit0, new Vector2(Game.Window.ClientBounds.Width - Exit0.Width,
                        Game.Window.ClientBounds.Height - Exit0.Height), Color.White);
                    spriteBatch.End();

                    break;
            }

            base.Draw(gameTime);
        }
    }
}
