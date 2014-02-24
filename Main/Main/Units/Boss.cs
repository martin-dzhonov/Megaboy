using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main.Interfaces;

namespace Main
{
    class Boss : Enemy
    {
        public Boss(int positonX, int positionY, int rectangleWidth = 130, int rectangleHeight = 130)
            : base(positonX, positionY, rectangleWidth, rectangleHeight)
        {
            this.Health = 100;
            this.spriteName = "Boss";
        }
  
    }
}
