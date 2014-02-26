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

        protected ContentManager conentManager;
        protected Rectangle sourceRectangle;

        protected bool lookingRight = true;

        protected float walkAnimationTimer = 0.30f;
        protected float attackAnimationTimer = 0.15f;
        protected int framesPerRow;
        protected int numRows;
        protected int frameHeight;
        protected int frameWidth;

        protected int detectionDistanceX = 350;
        protected int detectionDistanceY = 150;
        protected bool atacking = false;

        public int CurrentFrame { get; set; }
        public Melee(int positonX, int positionY)
            : base(positonX, positionY, 70, 50)
        {
            this.Health = 4;
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
