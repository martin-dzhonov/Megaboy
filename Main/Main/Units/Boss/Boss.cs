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
    class Boss : Ranged
    {


        public Boss(int positonX, int positionY, ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.rectangleSizeWidth = 120;
            this.rectangleSizeHeight = 120;
            this.Health = 30;
            this.spriteName = "bossWalking";
            this.texture = contentManager.Load<Texture2D>("bossWalking");
            this.conentManager = contentManager;
        }

        public override void Update(GameTime gameTime, int playerX, int playerY)
        {
            position += velocity;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);

            //patroling
            if (position.X > patrolPositon.X)
            {
                if ((int)(position.X - patrolPositon.X) > patrolDistance)
                {
                    lookingRight = false;
                    velocity.X = -1f;
                }
            }
            if (position.X < patrolPositon.X)
            {
                if (Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                {
                    lookingRight = true;
                    velocity.X = 1f;
                }
            }

            //detection
            playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;

            this.detectionDistanceX = 400;
            this.detectionDistanceY = 200;


            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX &&
                playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {

                if (playerDistanceX < 0)
                {
                    lookingRight = false;
                    velocity.X = 0f;
                }
                else if (playerDistanceX > 0)
                {
                    lookingRight = true;
                    velocity.X = 0f;
                }
            }
            //return to patroling
            else
            {

                if (lookingRight)
                {
                    velocity.X = 1f;
                }
                else
                {
                    velocity.X = -1f;
                }
            }

            //gravity
            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
            //animation
            if (velocity.X != 0)
            {
                this.AnimateWalking(gameTime, "bossWalking", 6, 1);
            }
            else
            {
                this.AnimateShooting(gameTime, "bossAttack", 4, 1);
            }
        }

    }
}
