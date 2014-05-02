using System;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Main.Units.Projectiles
{
    class Arrow : Projectile
    {
        public Arrow(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("ProjectileSprites//arrow");
            this.width = 40;
            this.height = 8;
        }
    }
}
