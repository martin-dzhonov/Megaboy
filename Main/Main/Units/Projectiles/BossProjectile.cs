using System;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Main.Units.Projectiles
{
    class BossProjectile : Projectile
    {
        public BossProjectile(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("ProjectileSprites//bossProjectile");
            this.width = 30;
            this.height = 30;
        }
    }
}
