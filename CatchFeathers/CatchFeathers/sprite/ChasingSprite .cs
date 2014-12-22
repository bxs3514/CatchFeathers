using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Feather_around_you.Conponent;

namespace Feather_around_you.sprite
{
    class ChasingSprite : AutomatedSprite
    {
        SpriteManage spriteManage;
        Vector2 speedTemp ;

        public ChasingSprite(Texture2D feather, Vector2 position,
            Vector2 speed, SpriteManage spriteManage, int score,
            Random  AutoCondition,Game gameControl)
            : base(feather, position, speed, score, AutoCondition, gameControl)
        {
            this.spriteManage = spriteManage;
            speedTemp = direction;
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

            float speedVal = Math.Max(Math.Abs(speedTemp.X), Math.Abs(speedTemp.Y)) ;

            if (Math.Abs(player.X - position.X) < 15 ||
                Math.Abs(position.Y - player.Y) < 15)
            {
                speed.X = speed.Y = 0;
                if (player.X < position.X)
                    position.X -= speedVal;
                if (player.X > position.X)
                    position.X += speedVal;
                if (player.Y < position.Y)
                    position.Y -= speedVal;
                if (player.Y > position.Y)
                    position.Y += speedVal;
            }
            else
            {
                speed = speedTemp;
            }

            base.update(gameTime, clientBounds);
        }
    }
}
