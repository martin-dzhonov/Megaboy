using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;
    public abstract class Unit : IUnit
    {        
        public Texture2D Strip { get; set; }

        public Rectangle SourceRectangle { get; set; }
        
        public abstract void Update(Microsoft.Xna.Framework.GameTime gameTime);

        public abstract void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager);

        public abstract void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
        
    }
}
