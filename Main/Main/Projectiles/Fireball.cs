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
    class Fireball : Projectile
    {     
        public Fireball(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("Fireball");
            this.width = 30;
            this.height = 30;
        }
    }
}
