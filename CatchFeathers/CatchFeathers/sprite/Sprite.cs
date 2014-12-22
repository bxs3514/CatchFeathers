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

namespace Feather_around_you.sprite
{
    abstract class Sprite
    {

        #region variable
        /// <summary>
        /// variables
        /// </summary>
        Texture2D feather1;
        
        int widthRegion1;
        int widthRegion2;
        int heightRegion1;
        int heightRegion2;
        protected int directCondition;
        int timeSinceLastFrame = 0;
        int score;
        int life;

        protected Vector2 position;
        protected Vector2 originalPosition;
        protected Vector2 speed;

        Random random = new Random();
        Random condiction = new Random();
        //random select the original position of the feather

        Game gameControl;
        #endregion

        protected Sprite(Texture2D feather1, Vector2 position, 
            Vector2 speed , int score, int life,Game gameControl)
        {
            this.feather1 = feather1;
            this.position = position;
            this.speed = speed;
            this.score = score;
            this.life = life;

            this.gameControl = gameControl;
            widthRegion1 = gameControl.Window.ClientBounds.Width / 3;
            widthRegion2 = gameControl.Window.ClientBounds.Width / 4 * 3;
            heightRegion1 = gameControl.Window.ClientBounds.Height / 3;
            heightRegion2 = gameControl.Window.ClientBounds.Height / 4 * 3;
        }


        #region ifOutScreen

        /// <summary>
        /// judge if the auto sprite out of screen
        /// </summary>
        public void dealTheOutCondition()
        {
            switch (condiction.Next(3))
            {
                case 0:
                    position.X = random.Next(widthRegion1, widthRegion2);
                    position.Y = 0;
                    originalPosition = position;
                    break;
                case 1:
                    position.X = 0;
                    position.Y = random.Next(heightRegion1, heightRegion2);
                    originalPosition = position;
                    break;
                case 2:
                    position.X = gameControl.Window.ClientBounds.Width - feather1.Width;
                    position.Y = random.Next(heightRegion1, heightRegion2);
                    originalPosition = position;
                    break;
                default:
                    break;
            }
        }

        public bool ifOutScreen()
        {
            if (position.X >= gameControl.Window.ClientBounds.Width + feather1.Width || 
                position.X + feather1.Width <= 0 ||
                position.Y >= gameControl.Window.ClientBounds.Height + feather1.Height|| 
                position.Y + feather1.Height<= 0)
            {
                dealTheOutCondition();
                return true;
            }
            else
                return false;
        }
         
        /// <summary>
        /// judge if userSprite out of screen
        /// </summary>
        /// <returns></returns>
        protected bool ifUserOutScreen()
        {
            if (position.X >= gameControl.Window.ClientBounds.Width + feather1.Width||
                position.X + feather1.Width<= 0 ||
                position.Y >= gameControl.Window.ClientBounds.Height + feather1.Height||
                position.Y + feather1.Height<= 0)
            {
                return false;
            }
            else
                return true;
        }
        #endregion

        public abstract Vector2 direction
        {
            get;
            set;
        }

        public Vector2 range
        {
            get
            {   return new Vector2(feather1.Width, feather1.Height); }
        }

        public Vector2 GetPosition
        {
            get
            { return position; }
        }

        public int GetDirectCondition
        {
            get
            { return directCondition; }
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle((int)position.X + 15, (int)position.Y + 15,
                    feather1.Width - 15, feather1.Height - 15);
            }
        }

        public int scoreValue
        {
            get
            { return score; }
            set
            { score = value; }
        }

        public int lifeValue
        {
            get { return life; }
            set { life = value; }
        }

        public virtual void update(GameTime gameTime,Rectangle clientBounds)
        {
            timeSinceLastFrame = gameTime.ElapsedGameTime.Milliseconds; 
        }
        
        public virtual void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(feather1, position, null, Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
        }
    }
}
