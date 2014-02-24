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

    abstract class NPC : Unit

    {
        protected string spriteName;
        public NPC(int positionX, int positionY);
        public abstract bool isReached(Player player);

        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(spriteName);
        }

        
    }
}
