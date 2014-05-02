using System;
using System.Linq;
using Main.MapAndBackGround;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Main.MapAndBackGround
{
    public class CollisionTile : Tiles
    {
        public CollisionTile(int i, Rectangle rectangle)
        {
            this.texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = rectangle;
        }
    }
}

namespace Main.MapAndBackGround
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
}
