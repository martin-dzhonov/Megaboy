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
        
        Rectangle sourceRectangle;
        private bool lookingRight;
        float timer = 2;
        float animationTimer = 0.25f;
        private int FRAMES_PER_ROW;
        private int NUM_ROWS;

        protected ContentManager conentManager;
        int frameHeight;
        int frameWidth;
        float interval = 30;
        public int CurrentFrame { get; set; }
        public Ranged(int positonX, int positionY, int rectangleWidth = 50, int rectangleHeight = 50) : base(positonX, positionY, rectangleWidth, rectangleHeight)
        { 

            // set initial source rectangle
            this.sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            this.Health = 2;
            
   
        }

        public override void Update(GameTime gameTime, int playerX, int playerY)
        {

            position += velocity;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);
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

            playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;

            int detectionDistanceX = 350;
            int detectionDistanceY = 200;

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

            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
            if (velocity.X != 0)
            {

                FRAMES_PER_ROW = 6;
                NUM_ROWS = 1;
                frameWidth = this.texture.Width / FRAMES_PER_ROW;
                frameHeight = this.texture.Height / NUM_ROWS;
               
                
                this.texture = conentManager.Load<Texture2D>("archerWalking");
                this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationTimer -= elapsed;
                if (animationTimer < 0)
                {
                    this.CurrentFrame++;
                    this.animationTimer = 0.25f;
                }
                if (this.CurrentFrame >= 6)
                {
                    this.CurrentFrame = 0;
                }
            }
            else 
            {
                FRAMES_PER_ROW = 4;
                NUM_ROWS = 1;
                frameWidth = this.texture.Width / FRAMES_PER_ROW;
                frameHeight = this.texture.Height / NUM_ROWS;
                
                this.texture = conentManager.Load<Texture2D>("archerShooting");
                
                this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationTimer -= elapsed;
                if (animationTimer < 0)
                {
                    this.CurrentFrame++;
                    this.animationTimer = 0.3f;
                }
                if (this.CurrentFrame > 4)
                {
                    this.CurrentFrame = 0;
                }
            }
        }

        public void Shoot(List<Projectile> projectiles, ContentManager contentManager, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
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

                timer = 2;
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
