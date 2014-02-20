﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Main
{
    
    public abstract class Unit : IUnit
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected Vector2 velocity;
        
        public abstract void Update(GameTime gameTime);

        public abstract void Load(ContentManager contentManager);

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}