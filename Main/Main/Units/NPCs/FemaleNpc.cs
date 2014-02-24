using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class FemaleNpc : NPC
    {

        public FemaleNpc(int positionX, int positionY) : base(positionX, positionY)
        {
            this.spriteName = "femaleNpc";
        }

        public override bool isReached(Player player)
        {
            if (((this.Position.X >= player.Position.X - 5) && (this.Position.X <= player.Position.X + 5)) &&
                this.Position.Y == player.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
