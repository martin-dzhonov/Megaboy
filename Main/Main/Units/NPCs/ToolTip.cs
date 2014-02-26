using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Main.Projectiles;

namespace Main
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
