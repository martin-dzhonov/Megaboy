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
    abstract class Melee : Enemy
    {
        private float attackTimer = 1f;
        protected ContentManager conentManager;
        protected Rectangle sourceRectangle;

        protected bool lookingRight = true;

        protected float animationTimer = 0.30f;
        protected int framesPerRow;
        protected int numRows;
        protected int frameHeight;
        protected int frameWidth;

        protected int detectionDistanceX = 350;
        protected int detectionDistanceY = 150;

        public int CurrentFrame { get; set; }
        public Melee(int positonX, int positionY)
            : base(positonX, positionY)
        {
            this.sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            this.Health = 2;
        }

        public override void Update(GameTime gameTime, int playerX, int playerY)
        {
            position += velocity;

            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);
            if (position.X > patrolPositon.X)
            {
                this.AnimateWalking(gameTime, "knightWalking", 6, 1);
                if ((int)(position.X - patrolPositon.X) > patrolDistance)
                {
                    velocity.X = -1f;
                    lookingRight = false;

                }
            }
            if (position.X < patrolPositon.X)
            {
                this.AnimateWalking(gameTime, "knightWalking", 6, 1);
                if (Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                {
                    velocity.X = 1f;
                    lookingRight = true;
                }
            }

            playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;

            int detectionDistanceX = 250;
            int detectionDistanceY = 200;

            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX
                && playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                attackTimer -= elapsed;
                if (attackTimer < 0)
                {
                    this.AnimateAttack(gameTime, "knightAttack", 4, 1);
                    if (CurrentFrame >= 4)
                    {
                        attackTimer = 1f;
                    }
                }
                if (playerDistanceX < 0)
                {
                    velocity.X = -1f;
                    patrolPositon.X -= 1;
                    lookingRight = false;
                }
                else if (playerDistanceX > 0)
                {
                    velocity.X = 1f;
                    patrolPositon.X += 1;
                    lookingRight = true;
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
        public void AnimateAttack(GameTime gameTime, string spriteName, int framesPerRow, int numRows)
        {
            this.animationTimer = 0.4f;
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
                this.animationTimer = 0.4f;
            }

            if (this.CurrentFrame > 4)
            {
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
