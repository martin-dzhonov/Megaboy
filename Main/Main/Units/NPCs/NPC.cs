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
        string spriteName;

        public NPC(string spriteName, int posX, int posY)
        {
            this.spriteName = spriteName;
            this.position = new Vector2(posX, posY);
            this.rectangle = new Rectangle(posX, posY, 90, 110);
        }

        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(spriteName);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if(this.rectangle.Intersects(player.Rectangle))
            {
                //talk
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
