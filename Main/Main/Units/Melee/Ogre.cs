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
    class Ogre : Melee
    {
        private float attackTimer = 1.5f;

        public Ogre(int positonX, int positionY, ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.rectangleSizeWidth = 100;
            this.rectangleSizeHeight = 120;
            this.Health = 8;
            this.texture = contentManager.Load<Texture2D>("ogreWalking");
            this.conentManager = contentManager;
            this.detectionDistanceX = 250;
            this.detectionDistanceY = 200;
        }

        public override void Update(GameTime gameTime, int playerX, int playerY)
        {

            position += velocity;

            if (atacking == false)
            {
                this.AnimateWalking(gameTime, "ogreWalking", 6, 1);
            }
            else
            {
                this.AnimateAttack(gameTime, "ogreAttack", 4, 1);

                if (this.CurrentFrame >= 4)
                {
                    atacking = false;
                }
            }
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);

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

                }
                else if (playerDistanceX > 0)
                {
                    velocity.X = 1f;

                }
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                attackTimer -= elapsed;
                if (attackTimer < 0)
                {
                    atacking = true;
                    attackTimer = 1.5f;
                }

            }
            else
            {
                if (position.X > patrolPositon.X)
                {
                    if ((int)(position.X - patrolPositon.X) > patrolDistance)
                    {
                        velocity.X = -1f;
                    }
                }
                else if (position.X < patrolPositon.X)
                {
                    if (Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                    {
                        velocity.X = 1f;
                    }
                }
            }

            if (hasJumped == true)
            {
                position.Y -= 4f;
                velocity.Y = -8f;
                hasJumped = false;
            }
            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
        }

    }
}
