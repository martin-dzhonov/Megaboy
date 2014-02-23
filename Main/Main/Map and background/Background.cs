using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Main.Enum;

namespace Main
{
    class Background
    {
        protected Texture2D texture;
        protected Rectangle rectangle;

    }
    class ContinuingBackground : Background
    {
        private int numberOfScreens;

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfScreens; i++)
            {
                this.rectangle = new Rectangle(i * (int)WindowSize.Width, 0, (int)WindowSize.Width, (int)WindowSize.Height);
                spriteBatch.Draw(texture, rectangle, Color.White);
            }

        }

        public void Load(ContentManager contentManager, int numberOfScreens)
        {
            this.texture = contentManager.Load<Texture2D>("Forest2");
            this.numberOfScreens = numberOfScreens;
        }
    }
}
