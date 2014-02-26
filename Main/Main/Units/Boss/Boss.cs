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
            this.patrolDistance = 500;
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

            this.detectionDistanceX = 420;
            this.detectionDistanceY = 120;


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
            if (hasJumped == true)
            {
                position.Y -= 4f;
                velocity.Y = -8f;
                hasJumped = false;
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

      
        public override void Shoot(List<Projectile> projectiles, ContentManager contentManager, GameTime gameTime)
        {
            if (this.CurrentFrame == 3 && this.velocity.X == 0)
            {
                Projectile fireball = new BossProjectile(contentManager);
                if (this.lookingRight)
                {
                    fireball.ShootRight();
                }
                else
                {
                    fireball.ShootLeft();
                }
                Projectile fireball1 = new BossProjectile(contentManager);
                if (this.lookingRight)
                {
                    fireball1.ShootRight();
                }
                else
                {
                    fireball1.ShootLeft();
                }
                Projectile fireball2 = new BossProjectile(contentManager);
                if (this.lookingRight)
                {
                    fireball2.ShootRight();
                }
                else
                {
                    fireball2.ShootLeft();
                }

                fireball.Position = new Vector2((int)this.Position.X, (int)this.Position.Y + 0) + fireball.Velocity * 3;
                fireball1.Position = new Vector2((int)this.Position.X, (int)this.Position.Y + 20) + fireball.Velocity * 3;
                fireball2.Position = new Vector2((int)this.Position.X, (int)this.Position.Y + 40) + fireball.Velocity * 3;
                projectiles.Add(fireball);
                projectiles.Add(fireball1);
                projectiles.Add(fireball2);
                this.CurrentFrame = 0;
            }
        }

    }
}
