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
    class Animation
    {
        Texture2D strip;
        Vector2 position;
        Rectangle sourceRectangle;
        Rectangle drawRectangle;
        public bool LookingRight { get; private set; }
        int currentFrame;
        int frameHeight;
        int frameWidth;
        float timer;
        float interval = 60;

        const int FRAMES_PER_ROW = 8;
        const int NUM_ROWS = 1;
        const int NUM_FRAMES = 8;

        private const string STRIP_NAME = "16jpo1w";
        private const int PLAYER_SIZE = 60;

        public void Load(ContentManager contentManager)
        {
            strip = contentManager.Load<Texture2D>(STRIP_NAME);

            // calculate frame size
            frameWidth = strip.Width / FRAMES_PER_ROW;
            frameHeight = strip.Height / NUM_ROWS;

            // set initial draw and source rectangles
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            drawRectangle = new Rectangle((int)position.X, (int)position.Y, PLAYER_SIZE, PLAYER_SIZE);
            spriteBatch.Draw(strip, drawRectangle, sourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime, int playerX, int playerY)
        {
            sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            position.X = playerX;
            position.Y = playerY;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.LookingRight = true;
                AnimateRight(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.LookingRight = false;
                AnimateLeft(gameTime);
            }
        }

        public void AnimateRight(GameTime gameTime)
        {
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
    }
}
