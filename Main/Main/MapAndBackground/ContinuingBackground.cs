using Main.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Main.MapAndBackground
{
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
            this.texture = contentManager.Load<Texture2D>("BackgroundSprites//Forest2");
            this.numberOfScreens = numberOfScreens;
        }
    }
}