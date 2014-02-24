namespace Main
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public interface IMovable
    {
        float Timer { get; set; }
        float Interval { get; set; }
        int CurrentFrame { get; set; }
        void AnimateRight(GameTime gameTime);
        void AnimateLeft(GameTime gameTime);
    }
}
