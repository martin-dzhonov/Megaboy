namespace Main
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public interface IUnit
    {
        void Collision(Rectangle newRectangle, int xOffset, int yOffset);
        void Update(GameTime gameTime);
        void Load(ContentManager contentManager);
        void Draw(SpriteBatch spriteBatch);
    }
}
