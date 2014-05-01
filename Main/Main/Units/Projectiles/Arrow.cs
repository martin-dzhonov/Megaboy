using System;
using System.Collections.Generic;
using System.Linq;
using Main.Units.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Main.Projectiles
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
