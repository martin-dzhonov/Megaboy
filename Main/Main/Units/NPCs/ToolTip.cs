using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Main.Units.NPCs
{
    public class ToolTip
    {
        public bool IsVisible { get; set; }
        protected Texture2D texture;
        protected string spriteName;
        protected Rectangle rectangle;
        protected Vector2 position;

        public ToolTip(ContentManager content, string spriteName, int posX, int posY, int width, int hight)
        {
            this.texture = content.Load<Texture2D>(spriteName);
            this.position = new Vector2(posX, posY);
            this.rectangle = new Rectangle(posX, posY, width, hight);
        }
        
        public void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(spriteName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }

        }
    }
}
