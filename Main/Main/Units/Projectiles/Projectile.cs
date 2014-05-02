using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Main.Units.Projectiles
{
    public abstract class Projectile
    {
        protected Texture2D texture;
        protected Rectangle rectangle;
        protected Vector2 position;
        protected Vector2 velocity;
        protected int width;
        protected int height;

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
            set
            {
                this.rectangle = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public void UpdatePosition()
        {
            this.position += this.velocity;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, this.width, this.height);
        }

        public void ShootLeft()
        {
            this.Velocity = new Vector2(-10, 0);
        }
        public void ShootRight()
        {
            this.Velocity = new Vector2(10, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0f);
            }
            else
            {
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            }
        }
    }
}
