using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Main
{
    class Explosion
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Rectangle sourceRectangle;
        private bool finished;

        const int FRAMES_PER_ROW = 7;
        const int NUM_ROWS = 1;
        const int NUM_FRAMES = 7;
        private int currentFrame;
        int frameHeight;
        int frameWidth;

        public Explosion(ContentManager contentManger,int xCoord, int yCoord)
        {
            this.texture = contentManger.Load<Texture2D>("explosion");
            this.rectangle = new Rectangle(xCoord, yCoord, 50,50);

            frameWidth = this.texture.Width / FRAMES_PER_ROW;
            frameHeight = this.texture.Height / NUM_ROWS;
        }

        public bool Finished
        {
            get
            {
                return this.finished;
            }
            set
            {
                this.finished = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sourceRectangle = new Rectangle(this.currentFrame * frameWidth, 0, frameWidth, frameHeight);
            spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White);
            currentFrame++;
            if(currentFrame > 7)
            {
                finished = true;
            }
        }
    }
}
