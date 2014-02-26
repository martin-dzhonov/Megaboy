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
using Main.Interfaces;
using Main.Exceptions;
namespace Main
{
    abstract class Enemy : Unit, IHealth
    {
        protected float playerDistanceX;
        protected float playerDistanceY;
        protected Vector2 patrolPositon;
        protected int patrolDistance;
        protected bool hasJumped = false;
        protected string spriteName = "archerShooting1";
        
        protected int rectangleSizeWidth;
        protected int rectangleSizeHeight;
        private int health;

        public Enemy(int positionX, int positionY)
        {
            this.position.X = positionX;
            this.position.Y = positionY;
            this.patrolPositon.X = positionX;
            this.patrolPositon.Y = positionY;
            this.patrolDistance = 50;
            this.velocity.X = 1f;
            this.velocity.Y = 1f;
        }

        public Enemy(int positionX, int positionY, int rectangleWidth , int rectangleHeight)
        {
            this.position.X = positionX;
            this.position.Y = positionY;
            this.patrolPositon.X = positionX;
            this.patrolPositon.Y = positionY;
            this.patrolDistance = 50;
            this.velocity.X = 1f;
            this.velocity.Y = 1f;
            this.rectangleSizeWidth = rectangleWidth;
            this.rectangleSizeHeight = rectangleHeight;
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if (value <= -1000)
                {
                    throw new InvalidHealthException("Invalid enemy health.");
                }
                this.health = value;
            }
        }

        public abstract void Update(GameTime gameTime, int playerX, int playerY);

        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(spriteName);
        }

        public virtual void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {

            if (rectangle.TouchTopOf(newRectangle))
            {
                velocity.Y = 0f;

            }
            if (rectangle.TouchLeftOff(newRectangle))
            {
                hasJumped = true;
                patrolPositon.X -= 10;
            }
            if (rectangle.TouchRightOff(newRectangle))
            {
                hasJumped = true;
                patrolPositon.X += 10;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X > xOffset - rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
            }
            if (position.Y < 0)
            {
                velocity.Y = 1f;
            }
            if (position.Y > yOffset - rectangle.Height)
            {
                position.Y = yOffset - rectangle.Height;
            }
        }
       
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
                spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

    }
}
