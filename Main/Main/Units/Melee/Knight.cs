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
    class Knight : Ranged
    {
        private float attackTimer = 1f;

        public Knight(int positonX, int positionY, ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.rectangleSizeWidth = 60;
            this.rectangleSizeHeight = 70;
            this.Health = 2;
            this.spriteName = "knightWalking";
            this.conentManager = contentManager;
        }

        public override void Update(GameTime gameTime, int playerX, int playerY)
        {
            position += velocity;

            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangleSizeWidth, rectangleSizeHeight);
            if (position.X > patrolPositon.X)
            {
                this.AnimateWalking(gameTime, "knightWalking", 6, 1);
                if ((int)(position.X - patrolPositon.X) > patrolDistance)
                {
                    velocity.X = -1f;
                    lookingRight = false;
                }
            }
            if (position.X < patrolPositon.X)
            {
                this.AnimateWalking(gameTime, "knightWalking", 6, 1);
                if (Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                {
                    velocity.X = 1f;
                    lookingRight = true;
                }
            }

            playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;

            int detectionDistanceX = 250;
            int detectionDistanceY = 200;

            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX
                && playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                attackTimer -= elapsed;
                if (attackTimer < 0)
                {
                    this.AnimateShooting(gameTime, "knightAttack", 4, 1);
                    if (CurrentFrame >= 4)
                    {
                        attackTimer = 1f;

                    }
                }
                if (playerDistanceX < 0)
                {
                    velocity.X = -1f;
                    patrolPositon.X -= 1;
                    lookingRight = false;
                }
                else if (playerDistanceX > 0)
                {
                    velocity.X = 1f;
                    patrolPositon.X += 1;
                    lookingRight = true;
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
