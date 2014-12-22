using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Materials
    {
        public SpriteBatch spriteBatch;
        public GraphicsDevice graphicsDevice;
        //public GraphicsDeviceManager graphics;
        //public Random rnd;

        public Materials(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);

        }
    }
}
