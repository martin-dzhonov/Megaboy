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
    class BossProjectile : Projectile
    {
        public BossProjectile(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("bossProjectile");
            this.width = 30;
            this.height = 30;
        }
    }
}
