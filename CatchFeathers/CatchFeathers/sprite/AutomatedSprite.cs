using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Feather_around_you.sprite
{
    class AutomatedSprite : Sprite
    {
        Random condition;//random select how the feather move

        public AutomatedSprite(Texture2D feather, Vector2 position,
            Vector2 speed , int score, Random condition, Game gameControl)
            : base(feather, position, speed, score, 0, gameControl)
        {
            this.condition = condition;
            directCondition = condition.Next(2);
        }

        public override Vector2 direction
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        protected void moveWay()
        {
            position += speed;
        }


        public override void update(GameTime gameTime, Rectangle clientBounds)
        {
            moveWay();
            base.update(gameTime, clientBounds);
        }
    }
}
