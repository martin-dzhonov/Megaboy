using System;
using System.Collections.Generic;
using System.Linq;
using Main;
using Microsoft.Xna.Framework.Content;

namespace Main.Units.Ranged
{
    class Archer : Ranged
    {               
        public Archer(int positonX, int positionY,ContentManager contentManager)
            : base(positonX, positionY)
        {         
            this.conentManager = contentManager;
            this.rectangleSizeWidth = 90;
            this.rectangleSizeHeight = 70;

            this.currentSpriteName = "EnemySprites\\archer\\walking";
            this.walkingSpriteName = "EnemySprites\\archer\\walking";
            this.attackingSpriteName = "EnemySprites\\archer\\attack";

            this.Health = 3;          
        } 
    }
}
