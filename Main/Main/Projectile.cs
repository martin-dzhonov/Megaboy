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
    class Projectile
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 velocity;
        private bool isVisible;


        public Projectile(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("Projectile");
            this.isVisible = false;
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

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                this.isVisible = value;
            }
        }

        public bool Collided(Rectangle newRectangle)
        {
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (rectangle.TouchTopOf(newRectangle))
            {
                return true;
            }
            if (rectangle.TouchLeftOff(newRectangle))
            {
                return true;
            }
            if (rectangle.TouchRightOff(newRectangle))
            {
                return true;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                return true;
            }
            return false;
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
            spriteBatch.Draw(texture, position, null, Color.White);//, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
