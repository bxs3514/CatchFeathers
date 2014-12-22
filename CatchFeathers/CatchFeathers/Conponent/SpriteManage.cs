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
    public class SpriteManage : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SoundEffect collisionSound;
        SoundEffect collisionSound2;
        SoundEffect flameCollision;
        SoundEffectInstance flameCollision2;
        SoundEffect getLife;
        SoundEffectInstance getLife2;
        SpriteFont scoreFont;
        SpriteFont lifeFont;
        SpriteFont levelFont;
        userSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        Texture2D feather1;
        Texture2D feather2;
        Texture2D feather3;
        Texture2D torch;
        Texture2D life;

        int minMilliseconds = 1000;
        int maxMilliseconds = 1500;
        int minSpeed = 2;
        int minSpeed2 = 5;
        int maxSpeed = 6;
        int maxSpeed2 = 10;
        int[] nextSpawnTime = new int[5];
        int widthRegion1;
        int widthRegion2;
        int heightRegion1;
        int heightRegion2;
        int orginalScore;

        int percentageAutomatic = 65;
        int percentageChasing = 25;
        int percentageAddingLife = 5;
        //int percentageEvading = 5;

        public SpriteManage(Game game)
            : base(game)
        {
            // TODO: Construct any child components here

            for (int i = 0; i < 4; i++)
                nextSpawnTime[i] = 0;

            widthRegion1 = game.Window.ClientBounds.Width / 3;
            widthRegion2 = game.Window.ClientBounds.Width / 4 * 3;
            heightRegion1 = game.Window.ClientBounds.Height / 3;
            heightRegion2 = game.Window.ClientBounds.Height / 4 * 3;
        }

        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }

        public int GetScore
        {
            get { return player.scoreValue;}
            set { player.scoreValue = value; }
        }

        public int GetLife
        {
            get { return player.lifeValue; }
            set { player.lifeValue = value; }
        }

        public int GetLevel
        {
            get { return player.levelvalue; }
            set { player.levelvalue = value; }
        }

        private void ResetSpawnTime(int i)
        {
            nextSpawnTime[i] = ((Game1)Game).rnd.Next(minMilliseconds, maxMilliseconds);
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
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            scoreFont = Game.Content.Load<SpriteFont>(@"Font\Score");
            feather1 = Game.Content.Load<Texture2D>(@"material\feather1");
            feather2 = Game.Content.Load<Texture2D>(@"material\feather2");
            feather3 = Game.Content.Load<Texture2D>(@"material\feather3");
            torch = Game.Content.Load<Texture2D>(@"material\bullet");

            player = new userSprite(Game.Content.Load<Texture2D>(@"material\catch"),
                new Vector2(Game.Window.ClientBounds.Width / 2 , 
                    Game.Window.ClientBounds.Height / 2 ),
                    new Vector2(6f,6f), 5, Game);

            lifeFont = Game.Content.Load<SpriteFont>(@"Font\Life");
            levelFont = Game.Content.Load<SpriteFont>(@"Font\Level");
            life = Game.Content.Load<Texture2D>(@"material\life");
            
            collisionSound = Game.Content.Load<SoundEffect>(@"Audio\collision");
            collisionSound2 = Game.Content.Load<SoundEffect>(@"Audio\collision2");
            flameCollision = Game.Content.Load<SoundEffect>(@"Audio\flame1");
            getLife = Game.Content.Load<SoundEffect>(@"Audio\getLife");

            flameCollision2 = flameCollision.CreateInstance();
            getLife2 = getLife.CreateInstance();
            base.LoadContent();
        }

        private void spawnSprite()
        {
            int appearType = ((Game1)Game).rnd.Next(100);
            int ifAddLifeAppera = ((Game1)Game).rnd.Next(100);
            Vector2 position = Vector2.Zero;
            Vector2 speed = Vector2.Zero;
            Vector2 speed2 = Vector2.Zero;

            switch (((Game1)Game).rnd.Next(3))
            {
                case 0:
                    position.X = ((Game1)Game).rnd.Next(widthRegion1, widthRegion2);
                    position.Y = 0;
                    break;
                case 1:
                    position.X = 0;
                    position.Y = ((Game1)Game).rnd.Next(heightRegion1, heightRegion2);
                    break;
                case 2:
                    position.X = Game.Window.ClientBounds.Width - feather2.Width;
                    position.Y = ((Game1)Game).rnd.Next(heightRegion1, heightRegion2);
                    break;
                default:
                    break;
            }

            #region moveway

            if (position.Y == 0)
            {
                switch (((Game1)Game).rnd.Next(2))
                {
                    case 0:
                        speed.X = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed.Y = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed2.X = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        speed2.Y = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2); 
                        break;
                    case 1:
                        speed.X = -((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed.Y = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed2.X = -((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        speed2.Y = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2); 
                        break;
                    default:
                        break;
                }
            }

            else if (position.X == 0)
            {
                switch (((Game1)Game).rnd.Next(2))
                {
                    case 0:
                        speed.X = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed.Y = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed2.X = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        speed2.Y = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2); 
                        break;
                    case 1:
                        speed.X = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed.Y = -((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed2.X = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        speed2.Y = -((Game1)Game).rnd.Next(minSpeed2, maxSpeed2); 
                        break;
                    default:
                        break;
                }
            }

            else if (position.X == Game.Window.ClientBounds.Width - feather2.Width)
            {
                switch (((Game1)Game).rnd.Next(2))
                {
                    case 0:
                        speed.X = -((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed.Y = ((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed2.X = -((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        speed2.Y = ((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        break;
                    case 1:
                        speed.X = -((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed.Y = -((Game1)Game).rnd.Next(minSpeed, maxSpeed);
                        speed2.X = -((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        speed2.Y = -((Game1)Game).rnd.Next(minSpeed2, maxSpeed2);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            if (appearType < percentageAutomatic / 2)
            {
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"material\feather2"),
                    position, speed, 0, ((Game1)Game).rnd, Game));
                
            }
            else if (appearType < percentageAutomatic)
            {
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"material\feather1"),
                    position, speed * 1.5f, 0, ((Game1)Game).rnd, Game));
            }
            else if (appearType < percentageAutomatic + percentageChasing)
            {
                spriteList.Add(new ChasingSprite(
                    Game.Content.Load<Texture2D>(@"material\bullet"),
                    position, speed * 2, this, 0, ((Game1)Game).rnd, Game));
            }
            else
            {
                spriteList.Add(new EvadingSprite(
                    Game.Content.Load<Texture2D>(@"material/feather3"),
                    position, speed2, this, 0, ((Game1)Game).rnd, Game));
            }

            if (player.lifeValue <= 2)
            {
                if (appearType < percentageAddingLife)
                {
                    speed2.Y *= 3;
                    spriteList.Add(new AddingLifeSprite(
                        Game.Content.Load<Texture2D>(@"material\needleWork"),
                        position, speed2, ((Game1)Game).rnd, Game));
                }
            }
        }
        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            orginalScore = player.scoreValue;

            getLife2.Stop();
            flameCollision2.Stop();

            for (int i = 0; i < 4; i++)
            nextSpawnTime[i] -= gameTime.ElapsedGameTime.Milliseconds;

            if (nextSpawnTime[0] < 0)
            {
                for(int i = 1; i <= 5; i++)
                    spawnSprite();
                ResetSpawnTime(0);
            }
            if (player.scoreValue >= 500 && player.scoreValue < 1000)
            {
                player.levelvalue = 1;
                if (nextSpawnTime[1] < 0)
                {
                    for(int i = 1; i <= 10; i++)
                        spawnSprite();

                    ResetSpawnTime(1);
                }
            }
            if (player.scoreValue >= 1000 && player.scoreValue <3000)
            {
                player.levelvalue = 2;
                if (nextSpawnTime[2] < 0)
                {
                    for (int i = 1; i <= 15; i++)
                        spawnSprite();

                    ResetSpawnTime(2);
                }
            }
            if (player.scoreValue >= 3000 && player.scoreValue < 5000)
            {
                player.levelvalue = 3;
                if (nextSpawnTime[3] < 0)
                {
                    for (int i = 1; i <= 20; i++)
                        spawnSprite();

                    ResetSpawnTime(3);
                }
            }
            if (player.scoreValue >= 5000 && player.scoreValue < 10000)
            {
                player.levelvalue = 4;
                if (nextSpawnTime[4] < 0)
                {
                    for (int i = 1; i <= 25; i++)
                        spawnSprite();

                    ResetSpawnTime(4);
                }
            }
            if (player.scoreValue >= 10000)
            {
                this.Enabled = false;
                this.Visible = false;
                ((Game1)Game).gameState = GameState.Over;
            }

            player.update(gameTime, Game.Window.ClientBounds);

            for(int i = 0; i < spriteList.Count ; i++)
            {
                Sprite s = spriteList[i];
                s.update(gameTime, Game.Window.ClientBounds);

                if (s.ifOutScreen() == true)
                {
                    try
                    {
                        if (i >= 0)
                        {
                            spriteList.RemoveAt(i);
                            i--;
                        }
                    }
                    catch (IndexOutOfRangeException indexOut)
                    {
                        Console.WriteLine("{0}", indexOut.Message);
                        ((Game1)Game).gameState = GameState.Over;
                    }
                }

                if (s.collisionRect.Intersects(player.collisionRect))
                {
                    if (s.GetType() == typeof(AutomatedSprite))
                    {
                        collisionSound.Play();
                        player.scoreValue += 50;
                    }
                    else if (s.GetType() == typeof(EvadingSprite))
                    {
                        collisionSound2.Play();
                        player.scoreValue += 300;
                    }
                    else if (s.GetType() == typeof(AddingLifeSprite))
                    {
                        getLife2.Play();
                        player.lifeValue++;
                    }
                    else if (s.GetType() == typeof(ChasingSprite))
                    {
                        flameCollision2.Play();
                        player.lifeValue--;
                    }

                    try
                    {
                        if (i >= 0)
                        {
                            spriteList.RemoveAt(i);
                            i--;
                        }
                    }
                    catch (IndexOutOfRangeException indexOut)
                    {
                        Console.WriteLine("{0}", indexOut.Message);
                        ((Game1)Game).gameState = GameState.Over;
                    }
                }

            }

            if (((Game1)Game).gameState == GameState.Start)
            {
                getLife2.Stop();
                flameCollision2.Stop();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int i = orginalScore; i <= player.scoreValue; i++)
            {
                spriteBatch.DrawString(scoreFont, "Score:" + orginalScore,
                    new Vector2(10, 10), Color.DarkBlue, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 1);
                orginalScore++;
            }

            spriteBatch.DrawString(lifeFont, "life:" ,
                new Vector2(10, 30), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(levelFont, "level:" + player.levelvalue,
                new Vector2(Game.Window.ClientBounds.Width / 2 - levelFont.MeasureString("level").X / 2
                    , 10),Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            player.draw(gameTime, spriteBatch);

            foreach (Sprite s in spriteList)
            {
                s.draw(gameTime, spriteBatch);
            }
            for (int i = 0; i < player.lifeValue; i++)
            {
                spriteBatch.Draw(life, new Vector2(60 + i * 25, 30), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
