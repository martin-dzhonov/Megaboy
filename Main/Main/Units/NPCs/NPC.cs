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
        public ToolTip toolTip;

        public NPC(string spriteName, int posX, int posY, ToolTip toolTip)
        {
            this.spriteName = spriteName;
            this.position = new Vector2(posX, posY);
            this.rectangle = new Rectangle(posX, posY, 90, 110);
            this.toolTip = toolTip;
        }

        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(spriteName);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (player.Rectangle.Intersects(this.rectangle))
            {
                toolTip.IsVisible = true;
            }
            else
            {
                toolTip.IsVisible = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
