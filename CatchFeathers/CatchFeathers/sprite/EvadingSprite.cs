using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Feather_around_you.Conponent;

namespace Feather_around_you.sprite
{
    class EvadingSprite : AutomatedSprite
    {
        SpriteManage spriteManage;
        Vector2 speedTemp;

        public EvadingSprite(Texture2D feather, Vector2 position,
            Vector2 speed, SpriteManage spriteManage, int score,
            Random EvadingCondition, Game gameControl)
            : base(feather, position, speed, score, EvadingCondition, gameControl)
        {
            this.spriteManage = spriteManage;
            speedTemp = speed;
        }

        public override Vector2 direction
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        public override void update(GameTime gameTime, Rectangle clientBounds)
        {
            Vector2 player = spriteManage.GetPlayerPosition();

            float speedVal = Math.Max(Math.Abs(speedTemp.X), Math.Abs(speedTemp.Y));

            if (Math.Abs(player.X - position.X) < 50 &&
                Math.Abs(player.Y - position.Y) < 70)
            {
                if (player.X > position.X && player.Y > position.Y)//upleft
                {
                    if (speed.X > 0)
                        speed.X = speedTemp.X * (-1.5f);
                    else
                    {
                        if (speed.X == speedTemp.X)
                            speed.X = speed.X * 1.5f;
                    }

                    if (speed.Y > 0)
                        speed.Y = speedTemp.Y * (-1.5f);
                    else
                    {
                        if (speed.Y == speedTemp.Y)
                            speed.Y = speed.Y * 1.5f;
                    }
                }
                if (player.X < position.X && player.Y > position.Y)//upright
                {
                    if (speed.X < 0)
                        speed.X =  speedTemp.X * (-1.5f);
                    else
                    {
                        if (speed.X == speedTemp.X)
                            speed.X = speed.X * 1.5f;
                    }

                    if (speed.Y > 0)
                        speed.Y = speedTemp.Y * (-1.5f);
                    else
                    {
                        if (speed.Y == speedTemp.Y)
                            speed.Y = speed.Y * 1.5f;
                    }
                }
                if (player.X > position.X && player.Y < position.Y)//downleft
                {
                    if (speed.X > 0)
                        speed.X = speedTemp.X * (-1.5f);
                    else
                    {
                        if (speed.X == speedTemp.X)
                            speed.X = speed.X * 1.5f;
                    }

                    if (speed.Y > 0)
                        speed.Y = speedTemp.Y * (-1.5f);
                    else
                    {
                        if (speed.Y == speedTemp.Y)
                            speed.Y = speed.Y * 1.5f;
                    }
                }
                if (player.X < position.X && player.Y < position.Y)//downright
                {
                    if (speed.X < 0)
                        speed.X = speedTemp.X * (-1.5f);
                    else
                    {
                        if (speed.X == speedTemp.X)
                            speed.X = speed.X * 1.5f;
                    }

                    if (speed.Y > 0)
                        speed.Y = speedTemp.Y * (-1.5f);
                    else
                    {
                        if (speed.Y == speedTemp.Y)
                            speed.Y = speed.Y * 1.5f;
                    }
                }
            }
          
            base.update(gameTime, clientBounds);
        }
    }
}
