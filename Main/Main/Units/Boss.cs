using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    class Boss : Enemy
    {
        private int health = 100;

        public Boss(int positonX, int positionY, int rectangleWidth = 130, int rectangleHeight = 130)
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
