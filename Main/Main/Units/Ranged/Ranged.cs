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
using Main.Projectiles;

namespace Main
{
    abstract class Ranged : Enemy
    {       
        protected Rectangle sourceRectangle;
        protected bool lookingRight = true;
        protected float animationTimer = 0.20f;
        protected int framesPerRow;
        protected int numRows;

        protected ContentManager conentManager;
        protected int frameHeight;
        protected int frameWidth;

        public int CurrentFrame { get; set; }
        public Ranged(int positonX, int positionY) : base(positonX, positionY)
        { 
            this.sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            this.Health = 2;
        }

        public override void Update(GameTime gameTime, int playerX, int playerY)
        {

            
            position += velocity;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);

            //patroling
            if (position.X > patrolPositon.X)
            {
                if ((int)(position.X - patrolPositon.X) > patrolDistance)
                {
                    lookingRight = false;
                    velocity.X = -1f;
                }
            }
            if (position.X < patrolPositon.X)
            {
                if (Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                {
                    lookingRight = true;
                    velocity.X = 1f;
                }
            }

            //detection
            playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;

            int detectionDistanceX = 350;
            int detectionDistanceY = 150;

            
            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX &&
                playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {
                if (playerDistanceX < 0)
                {
                    lookingRight = false;
                    velocity.X = 0f;
                }
                else if (playerDistanceX > 0)
                {
                    lookingRight = true;
                    velocity.X = 0f;             
                }
            }
            //return to patroling
            else
            {
                if(lookingRight)
                {
                    velocity.X = 1f;
                }
                else
                {
                    velocity.X = -1f;
                }
            }
            
            //gravity
            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
            //animation
            if (velocity.X != 0)
            {
                AnimateWalking(gameTime, "",0 ,0);
            }
            else 
            {
                AnimateShooting(gameTime, "", 0, 0);
            }
        }
        public void AnimateWalking(GameTime gameTime, string spriteName, int framesPerRow, int numRows)
        {
            this.framesPerRow = framesPerRow;
            this.numRows = numRows;

            frameWidth = this.texture.Width / framesPerRow;
            frameHeight = this.texture.Height / numRows;


            this.texture = conentManager.Load<Texture2D>(spriteName);
            this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            animationTimer -= elapsed;

            if (animationTimer < 0)
            {
                this.CurrentFrame++;
                this.animationTimer = 0.20f;
            }

            if (this.CurrentFrame >= 6)
            {
                this.CurrentFrame = 0;
            }
        }
        public void AnimateShooting(GameTime gameTime, string spriteName, int framesPerRow, int numRows)
        {
            this.framesPerRow = framesPerRow;
            this.numRows = numRows;

            frameWidth = this.texture.Width / framesPerRow;
            frameHeight = this.texture.Height / numRows;

            this.texture = conentManager.Load<Texture2D>(spriteName);
            this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            animationTimer -= elapsed;

            if (animationTimer < 0)
            {
                this.CurrentFrame++;
                this.animationTimer = 0.40f;
            }

            if (this.CurrentFrame > 4)
            {
                this.CurrentFrame = 0;
            }
        }
        public void Shoot(List<Projectile> projectiles, ContentManager contentManager, GameTime gameTime)
        {
            if (this.CurrentFrame == 4 && this.velocity.X == 0)
            {
                Projectile fireball = new Arrow(contentManager);
                if (this.lookingRight)
                {
                    fireball.ShootRight();
                }
                else
                {
                    fireball.ShootLeft();
                }

                fireball.Position = new Vector2((int)this.Position.X, (int)this.Position.Y + 6) + fireball.Velocity * 3;

                projectiles.Add(fireball);

                this.CurrentFrame = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (lookingRight == true)
            {
                spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            }
        }
    }
}
