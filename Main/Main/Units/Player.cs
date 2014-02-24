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
using Main.Interfaces;
namespace Main
{
    class Player : Unit
    {
        private Texture2D standingTexture;
        private Texture2D runningTexture;
        private Texture2D shootingTexture;
        private Rectangle sourceRectangle;
        private bool hasJumped = false;
        private int health;
        const int FRAMES_PER_ROW = 15;
        const int NUM_ROWS = 1;
        const int NUM_FRAMES = 15;
        
    
        int frameHeight;
        int frameWidth;
        float interval = 30;
        

        public bool LookingRight { get; set; }
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;
            }
        }

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
            this.Health = 3;
            position = new Vector2(0, 0);
            this.runningTexture = contentManager.Load <Texture2D>("heroWalking2");
            this.standingTexture = contentManager.Load<Texture2D>("standing2");
            this.shootingTexture = contentManager.Load<Texture2D>("shooting1");
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

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
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
            if (this.CurrentFrame >= 15)
            {
                this.CurrentFrame = 0;
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
            if (this.CurrentFrame >= 15)
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
