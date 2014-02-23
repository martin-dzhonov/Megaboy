using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    class Boss : Ranged, Interfaces.IDyable
    {
        private int health = 100;

        public Boss(int positonX, int positionY, int rectangleWidth = 100, int rectangleHeight = 100)
            : base(positonX, positionY, rectangleWidth, rectangleHeight)
        {
            this.spriteName = "Boss";
        }
        public int Health
        {
            get
            {
                return this.health;
            }
        }
    }
}
