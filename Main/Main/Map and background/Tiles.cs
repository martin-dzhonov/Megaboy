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

namespace Main
{
    public class Tiles
    {
        protected Texture2D texture;
        public Rectangle Rectangle { get; set; }
        public static ContentManager Content { get; set; }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Rectangle, Color.White);
        }
    }

    public class CollisionTile : Tiles
    {
        public CollisionTile(int i, Rectangle rectangle)
        {
            this.texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = rectangle;
        }
    }
}
