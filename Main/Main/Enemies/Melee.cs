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
    class Melee : Enemy
    {
        float playerDistanceX;
        float playerDistanceY;
        Vector2 patrolPositon;
        int patrolDistance;

        public Melee(int positonX, int positionY)
        {
            this.position.X = positonX;
            this.position.Y = positionY;
            this.patrolPositon.X = positonX;
            this.patrolPositon.Y = positionY;
            this.patrolDistance = 50;
            this.velocity.X = 1f;
            this.velocity.Y = 1f;
        }
        public override void Update(GameTime gameTime, int playerX, int playerY)
        {
            
            position += velocity;
            
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, 50,50);
            if(position.X > patrolPositon.X)
            {
                if((int)(position.X - patrolPositon.X) > patrolDistance )
                {
                    velocity.X = -1f;
                }
            }
            if(position.X < patrolPositon.X)
            {
                if(Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                {
                    velocity.X = 1f;
                }
            }

          playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;
          
            int detectionDistanceX = 250;
            int detectionDistanceY = 200;

            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX
                && playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {
                if (playerDistanceX < 0)
                {
                    velocity.X = -1f;
                    patrolPositon.X -= 1;
                }
                else if (playerDistanceX > 0)
                {
                    velocity.X = 1f;
                    patrolPositon.X += 1;
                }
            }

            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
        }


        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("knight1");
        }

        public override void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
          
            if (rectangle.TouchTopOf(newRectangle))
            {
                velocity.Y = 0f;
            }
            if (rectangle.TouchLeftOff(newRectangle))
            {
                velocity.X = -1f;
                patrolPositon.X -= 10;
            }
            if (rectangle.TouchRightOff(newRectangle))
            {
                velocity.X = 1f;
                patrolPositon.X += 10;
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
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
