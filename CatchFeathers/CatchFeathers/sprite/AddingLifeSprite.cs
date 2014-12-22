using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Feather_around_you.sprite
{
    class AddingLifeSprite : AutomatedSprite
    {
        int waveMilliseconds = 150;

        public AddingLifeSprite(Texture2D feather, Vector2 position,
            Vector2 speed, Random condition, Game gameControl)
            : base(feather, position, speed, 0, condition, gameControl)
        { }

        public override void update(GameTime gameTime, Rectangle clientBounds)
        {
            waveMilliseconds -= gameTime.ElapsedGameTime.Milliseconds;
            if (waveMilliseconds < 0)
            {
                waveMilliseconds = 150;
                base.speed.Y *= -1;
            }

            base.update(gameTime, clientBounds);
        }
    }
}
