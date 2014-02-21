namespace Main.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        void Generate(int[,] map, int size);
        void Draw(SpriteBatch spriteBatch);
    }
}
