using System;
using System.Collections.Generic;
using System.Linq;
using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Main.Units.Melee
{
    abstract class Melee : Enemy
    {

        protected ContentManager conentManager;
        protected Rectangle sourceRectangle;

        private float walkAnimationTimer = 0.30f;
        private float attackAnimationTimer = 0.15f;
        private float triggerAttackTimer = 1.5f;

        private int framesPerRow;
        private int numRows;
        private int frameHeight;
        private int frameWidth;

        protected int detectionDistanceX;
        protected int detectionDistanceY;

        protected bool lookingRight = true;
        protected bool atacking = false;
        
        public int CurrentFrame { get; set; }
        public Melee(int positonX, int positionY)
            : base(positonX, positionY)
        {
        }
        
        public bool Atacking
        {
            get
            {
                return this.atacking;
            }
            set
            {
                this.atacking = value;
            }
        }
        public override void Update(GameTime gameTime, int playerX, int playerY)
        {
            position += velocity;

            if (atacking == false)
            {
                this.currentSpriteName = walkingSpriteName;
                this.AnimateWalking(gameTime, this.currentSpriteName, 6, 1);
            }
            else
            {
                this.currentSpriteName = attackingSpriteName;
                this.AnimateAttack(gameTime, this.currentSpriteName, 4, 1);

                if (this.CurrentFrame >= 4)
                {
                    atacking = false;
                }
            }
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);

            playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;

            int detectionDistanceX = 250;
            int detectionDistanceY = 200;

            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX
                && playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {
                if (playerDistanceX < 0)
                {
                    velocity.X = -1.3f;

                }
                else if (playerDistanceX > 0)
                {
                    velocity.X = 1.3f;

                }
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                triggerAttackTimer -= elapsed;
                if (triggerAttackTimer < 0)
                {
                    atacking = true;
                    triggerAttackTimer = 1.5f;
                }

            }
            else
            {
                if (position.X > patrolPositon.X)
                {
                    if ((int)(position.X - patrolPositon.X) > patrolDistance)
                    {
                        velocity.X = -1f;
                    }
                }
                else if (position.X < patrolPositon.X)
                {
                    if (Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                    {
                        velocity.X = 1f;
                    }
                }
            }

            if (hasJumped == true)
            {
                position.Y -= 4f;
                velocity.Y = -8f;
                hasJumped = false;
            }
            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
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
            walkAnimationTimer -= elapsed;

            if (walkAnimationTimer <= 0)
            {
                this.CurrentFrame++;
                walkAnimationTimer = 0.30f;
            } 

            if (this.CurrentFrame >= 6)
            {
                this.CurrentFrame = 0;
            }
        }

        public void AnimateAttack(GameTime gameTime, string spriteName, int framesPerRow, int numRows)
        {
            
            this.framesPerRow = framesPerRow;
            this.numRows = numRows;

            frameWidth = this.texture.Width / framesPerRow;
            frameHeight = this.texture.Height / numRows;

            this.texture = conentManager.Load<Texture2D>(spriteName);
            this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            attackAnimationTimer -= elapsed;

            if (attackAnimationTimer <= 0)
            {
                this.CurrentFrame++;
                attackAnimationTimer = 0.15f;
            }

            if (this.CurrentFrame > 4)
            {
                this.CurrentFrame = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.velocity.X > 0)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight), sourceRectangle, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight), sourceRectangle, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            }
        }

    }
}
