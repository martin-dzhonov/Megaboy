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
        public Ranged(int positonX, int positionY) : base(positonX, positionY)
        { 
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
                this.animationTimer = 0.30f;
            }

            if (this.CurrentFrame > 5)
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

            if (this.CurrentFrame > 3)
            {
                this.CurrentFrame = 0;
            }
        }
        public virtual void Shoot(List<Projectile> projectiles, ContentManager contentManager, GameTime gameTime)
        {
            if (this.CurrentFrame == 3 && this.velocity.X == 0)
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

                fireball.Position = new Vector2((int)this.Position.X, (int)this.Position.Y + 15) + fireball.Velocity * 3;

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
