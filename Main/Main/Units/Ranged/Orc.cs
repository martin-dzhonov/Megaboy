using System;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Main.Units.Ranged
{
    class Orc : Ranged
    {
        public Orc(int positonX, int positionY, ContentManager contentManager)
            : base(positonX, positionY)
        {
            this.conentManager = contentManager;
            this.rectangleSizeWidth = 90;
            this.rectangleSizeHeight = 70;

            this.currentSpriteName = "EnemySprites\\orc\\walking";
            this.walkingSpriteName = "EnemySprites\\orc\\walking";
            this.attackingSpriteName = "EnemySprites\\orc\\attack";

            this.Health = 4; 
        }
    }
}
