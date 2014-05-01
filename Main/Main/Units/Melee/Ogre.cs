using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;


namespace Main.Units.Melee
{
    class Ogre : Melee
    {
        public Ogre(int positonX, int positionY, ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.conentManager = contentManager;
            this.rectangleSizeWidth = 100;
            this.rectangleSizeHeight = 120;
            
            this.currentSpriteName = "EnemySprites\\ogre\\walking";
            this.walkingSpriteName = "EnemySprites\\ogre\\walking";
            this.attackingSpriteName = "EnemySprites\\ogre\\attack";
       
            this.Health = 8;
            this.detectionDistanceX = 300;
            this.detectionDistanceY = 200;
        }
    }
}
