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
    class Ranged : Enemy
    {
        public Ranged(int positonX, int positionY, int rectangleWidth = 50, int rectangleHeight = 50)
            : base(positonX, positionY, rectangleWidth, rectangleHeight)
        {
            this.Health = 2;
            this.spriteName = "ranged1";
        }
    }
}
