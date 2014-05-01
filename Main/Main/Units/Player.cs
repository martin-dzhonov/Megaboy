using System;
using System.Collections.Generic;
using System.Linq;

using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Main.Enum;
using Main.Interfaces;

namespace Main.Units
{
    class Player : Unit, IHealth
    {
        private Rectangle sourceRectangle;

        private Texture2D standingTexture;
        private Texture2D runningTexture;
        private Texture2D shootingTexture;
        
        private int currentFrame;
        private int framesPerRow = 15;
        private int numRows = 1;
        private int numFrames = 15;
        private int frameHeight;
        private int frameWidth;
        private float animationInterval;

        private bool hasJumped;

        public bool LookingRight { get; set; }
        public int Health { get; set; }
        public float Timer { get; set; }
        
        public Player()
        {
            this.Health = 35;
            this.animationInterval = 30;
            this.hasJumped = false;
        }

        public override void Load(ContentManager contentManager)
        {
            
            this.runningTexture = contentManager.Load<Texture2D>("PlayerSprites\\heroWalking2");
            this.standingTexture = contentManager.Load<Texture2D>("PlayerSprites\\standing2");
            this.shootingTexture = contentManager.Load<Texture2D>("PlayerSprites\\shooting1");

            this.texture = runningTexture;
            // calculate frame size
            frameWidth = this.texture.Width / framesPerRow;
            frameHeight = this.texture.Height / numRows;

            // set initial source rectangle
            this.sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }
        public override void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)PlayerSize.Width, (int)PlayerSize.Height);

            ReadInput(gameTime);

            //gravity
            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
        }

        private void ReadInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.texture = runningTexture;     
                this.LookingRight = true;                     
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                AnimateRunning(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.texture = runningTexture;     
                this.LookingRight = false;
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                AnimateRunning(gameTime);
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.X))
                {
                    this.texture = shootingTexture;
                }
                else
                {
                    this.texture = standingTexture;
                }
                this.sourceRectangle = new Rectangle(0, 0, standingTexture.Width, standingTexture.Height);
                velocity.X = 0f;

            }

            //jump
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
        }


        public void AnimateRunning(GameTime gameTime)
        {          
            this.sourceRectangle = new Rectangle(this.currentFrame * frameWidth, 0, frameWidth, frameHeight);
            this.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (this.Timer > animationInterval)
            {
                this.currentFrame++;
                this.Timer = 0;
            }
            if (this.currentFrame >= numFrames)
            {
                this.currentFrame = 0;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.TouchLeftOff(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOff(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
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
            if (velocity.X > 0 || LookingRight == true)
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
