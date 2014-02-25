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
     class Melee : Enemy
    {
        public Melee(int positonX, int positionY) : base (positonX, positionY)
        {       
            this.Health = 3;
            this.spriteName = "melee1";
        }
    }
}
