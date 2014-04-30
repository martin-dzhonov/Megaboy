using System;
using System.Collections.Generic;
using System.Linq;

using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Main.Enum;
using Microsoft.Xna.Framework.Media;
using Main.Interfaces;
using Main.Exceptions;

namespace Main
{
    class HUD
    {
        private Texture2D healthBarTexture;
        private Texture2D ammoBarTexture;
        private Texture2D healthSign;
        private Texture2D ammoSign;

        public void Load(ContentManager contentManager)
        {
            this.healthSign = contentManager.Load<Texture2D>("HUD sprites\\health");
            this.healthBarTexture = contentManager.Load<Texture2D>("HUD sprites\\redSquare");
            this.ammoSign = contentManager.Load<Texture2D>("HUD sprites\\ammo");
            this.ammoBarTexture = contentManager.Load<Texture2D>("HUD sprites\\greenSquare");                     
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, int playerHealth, int projectilesCount)
        {          
            spriteBatch.Draw(healthSign, new Rectangle((int)camera.Centre.X - 500, (int)camera.Centre.Y - 260, 150, 20), Color.White);
            spriteBatch.Draw(healthBarTexture, new Rectangle((int)camera.Centre.X - 500, (int)camera.Centre.Y - 235, playerHealth * 5, 12), Color.White);

            spriteBatch.Draw(ammoSign, new Rectangle((int)camera.Centre.X - 300, (int)camera.Centre.Y - 260, 150, 20), Color.White);
            spriteBatch.Draw(ammoBarTexture, new Rectangle((int)camera.Centre.X - 300, (int)camera.Centre.Y - 235, (5 - projectilesCount) * 30, 12), Color.White);
        }
    }
}
