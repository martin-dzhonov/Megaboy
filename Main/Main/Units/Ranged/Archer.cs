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
    class Archer : Ranged
    {
        public Archer(int positonX, int positionY,ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.rectangleSizeWidth = 50;
            this.rectangleSizeHeight = 50;
            this.Health = 2;
            this.spriteName = "archerWalking";
            this.conentManager = contentManager;
        }
    }
}
