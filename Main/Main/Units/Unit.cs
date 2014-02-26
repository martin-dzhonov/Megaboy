using System;
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
using Main.Interfaces;
namespace Main
{


     abstract class Unit : ILoad, IUpdate, IDraw
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected Vector2 velocity;
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
            set
            {
                this.rectangle = value;
            }
        }

        public abstract void Load(ContentManager contentManager);
        public virtual void Update(GameTime gameTime) {}
        public virtual void Update(GameTime gameTime, Player player) {}
        public virtual void Update(GameTime gameTime, int projectilesNum) {}
        public abstract void Draw(SpriteBatch spriteBatch);
        public virtual void Draw(SpriteBatch spriteBatch,Camera camera) {}
        
    }
}
