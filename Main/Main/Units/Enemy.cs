using System;
using System.Collections.Generic;
using System.Linq;

using Main.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Main.Interfaces;

namespace Main.Units
{
    abstract class Enemy : Unit, IHealth
    {
        protected int rectangleSizeWidth;
        protected int rectangleSizeHeight;

        protected string currentSpriteName = "EnemySprites\\archer\\walking";
        protected string walkingSpriteName = "";
        protected string attackingSpriteName = "";

        protected float playerDistanceX;
        protected float playerDistanceY;
        protected Vector2 patrolPositon;
        protected int patrolDistance;

        protected bool hasJumped = false;
              
        public int Health { get; set; }
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
      
        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(currentSpriteName);
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

        public abstract void Update(GameTime gameTime, int playerX, int playerY);


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
    }
}
