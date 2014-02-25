using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.StartMenu
{
    public class Button
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;

        public bool isClicked;
        public Vector2 size;

        public Button(ContentManager contentManager, string spriteName, int width, int height)
        {
            texture = contentManager.Load<Texture2D>(spriteName);

            size = new Vector2(width, height);
        }
        
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y,
               (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1,1);
            if(mouseRectangle.Intersects(rectangle))
            {
                if(mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
        }
        public void SetPosition(int xPos, int yPos)
        {
            this.position = new Vector2(xPos, yPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
