using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;


namespace Main.Units.Melee
{
    class Knight : Melee
    {
        public Knight(int positonX, int positionY, ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.conentManager = contentManager;
            this.rectangleSizeWidth = 70;
            this.rectangleSizeHeight = 80;
            
            this.currentSpriteName = "EnemySprites\\knight\\walking";
            this.walkingSpriteName = "EnemySprites\\knight\\walking";
            this.attackingSpriteName = "EnemySprites\\knight\\attack";

            this.Health = 5;
            this.detectionDistanceX = 300;
            this.detectionDistanceY = 200;
        }
    }
}
