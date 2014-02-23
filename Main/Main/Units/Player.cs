using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Main.Enum;
using Microsoft.Xna.Framework.Media;

namespace Main
{
    class Player : Unit, IMovable
    {
        private Texture2D standingTexture;
        private Texture2D runningTexture;
        private Rectangle sourceRectangle;
        private bool hasJumped = false;

        const int FRAMES_PER_ROW = 32;
        const int NUM_ROWS = 1;
        const int NUM_FRAMES = 32;
        
    
        int frameHeight;
        int frameWidth;
        float interval = 30;

        public bool LookingRight { get; set; }
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public float Timer { get; set; }

        public int CurrentFrame { get; set; }

        
        public override void Load(ContentManager contentManager)
        {
            position = new Vector2(0, 0);
            this.runningTexture = contentManager.Load <Texture2D>("running");
            this.standingTexture = contentManager.Load<Texture2D>("standing1");
            this.texture = runningTexture;
            // calculate frame size
            frameWidth = this.texture.Width / FRAMES_PER_ROW;
            frameHeight = this.texture.Height / NUM_ROWS;

            // set initial source rectangle
            this.sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

        }
        public override void Update(GameTime gameTime)
        {
            position += velocity;          
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)PlayerSize.Width, (int)PlayerSize.Height);

            ReadInput(gameTime);

            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
        }

        private void ReadInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.LookingRight = true;
                this.texture = runningTexture;
                AnimateRight(gameTime);
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                this.LookingRight = false;
                this.texture = runningTexture;
                AnimateLeft(gameTime);
            }
            else
            {
                this.texture = standingTexture;
                AnimateStanding(gameTime);
                velocity.X = 0f;
               
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
        }

        public void AnimateStanding(GameTime gameTime)
        {
            if (LookingRight)
            {
                 this.sourceRectangle = new Rectangle(0, 0, 60, this.standingTexture.Height);
            }         
            else
            {
                this.sourceRectangle = new Rectangle(60, 0, 60, this.standingTexture.Height);
            }
        }
        public void AnimateRight(GameTime gameTime)
        {
            this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);       
            this.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (this.Timer > interval)
            {
                this.CurrentFrame++;
                this.Timer = 0;
            }
            if (this.CurrentFrame < 16 || CurrentFrame >= 32)
            {
                this.CurrentFrame = 16;
            }
        }
        public void AnimateLeft(GameTime gameTime)
        {
            this.sourceRectangle = new Rectangle(this.CurrentFrame * frameWidth, 0, frameWidth, frameHeight);       
            this.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (this.Timer > interval)
            {
                this.CurrentFrame++;
                this.Timer = 0;
            }
            if (this.CurrentFrame > 15)
            {
                this.CurrentFrame = 0;
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
            spriteBatch.Draw(this.texture, rectangle, this.sourceRectangle, Color.White);
        }
    }
}
