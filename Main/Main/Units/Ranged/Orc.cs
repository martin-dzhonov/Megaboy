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

            this.Health = 3; 
        }

    }
}
