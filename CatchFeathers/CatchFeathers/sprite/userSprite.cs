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
    class userSprite : Sprite
    {
        private MouseState prevMouseState;
        int level;

        public userSprite(Texture2D feather, Vector2 position, 
            Vector2 speed , int life, Game gameControl)
            : base(feather, position, speed, 0, life, gameControl)
        {
            level = 0;
        }

        public int levelvalue
        {
            get { return level; }
            set { level = value; }
        }

        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    inputDirection.X -= 1;
                    if (ifUserOutScreen() == false)
                        inputDirection.X += 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    inputDirection.X += 1;
                    if (ifUserOutScreen() == false)
                        inputDirection.X -= 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    inputDirection.Y += 1;
                    if (ifUserOutScreen() == false)
                        inputDirection.X -= 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    inputDirection.Y -= 1;
                    if (ifUserOutScreen() == false)
                        inputDirection.Y += 1;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    return inputDirection * speed * 2;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    return inputDirection * speed * 3;
                }
                else
                {
                    return inputDirection * speed;
                }

            }
            set
            {
            }
        }

        public override void update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;

            MouseState currMouseState = Mouse.GetState();

            if (prevMouseState != currMouseState)
            {
                position = new Vector2(currMouseState.X, currMouseState.Y);
            }
            prevMouseState = currMouseState;

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - range.X)
                position.X = clientBounds.Width - range.X;
            if(position.Y>clientBounds.Height-range.Y)
                position.Y=clientBounds.Height-range.Y;
            base.update(gameTime, clientBounds);
        }
    }
}