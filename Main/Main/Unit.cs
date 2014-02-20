using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public abstract class Unit : IUnit
    {
        public abstract void Update(Microsoft.Xna.Framework.GameTime gameTime);

        public abstract void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager);

        public abstract void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);

    }
}
