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

namespace Main
{
    class Player : Unit
    {

        private Vector2 position = new Vector2(63, 63);
        private Vector2 velocity;
        private Rectangle rectangle;       
        Texture2D strip;
        Rectangle sourceRectangle;
             
        private bool hasJumped = false;
        private bool lookingRight;

        const int FRAMES_PER_ROW = 8;
        const int NUM_ROWS = 1;
        const int NUM_FRAMES = 8;
        private const string STRIP_NAME = "16jpo1w";
        private const int PLAYER_SIZE = 60;

        int currentFrame;
        int frameHeight;
        int frameWidth;
        float timer;
        float interval = 60;
        public bool LookingRight
        {
            get
            {
                return this.lookingRight;
            }
            private set
            {
                this.lookingRight = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public int X
        {
            get { return (int)this.position.X; }
        }

        public int Y
        {
            get { return (int)this.position.Y; }
        }

        public override void Load(ContentManager contentManager)
        {
            strip = contentManager.Load<Texture2D>(STRIP_NAME);

            // calculate frame size
            frameWidth = strip.Width / FRAMES_PER_ROW;
            frameHeight = strip.Height / NUM_ROWS;

            // set initial source rectangle
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

        }
        public override void Update(GameTime gameTime)
        {
            position += velocity;          
            rectangle = new Rectangle((int)position.X, (int)position.Y, PLAYER_SIZE, PLAYER_SIZE);

            ReadInput(gameTime);

            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }

        private void ReadInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.LookingRight = true;
                AnimateRight(gameTime);
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                this.LookingRight = false;
                AnimateLeft(gameTime);
            }
            else
            {
                velocity.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
        }

        public void AnimateRight(GameTime gameTime)
        {
            sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);       
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
            }
            if (currentFrame >= 4)
            {
                currentFrame = 0;
            }
        }
        public void AnimateLeft(GameTime gameTime)
        {
            sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);       
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
            }
            if (currentFrame > 7 || currentFrame < 4)
            {
                currentFrame = 4;
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
            spriteBatch.Draw(strip, rectangle, sourceRectangle, Color.White);
        }
    }
}
