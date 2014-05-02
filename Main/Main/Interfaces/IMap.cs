using Microsoft.Xna.Framework.Graphics;

namespace Main.Interfaces
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        void Generate(int[,] map, int size);
        void Draw(SpriteBatch spriteBatch);
    }
}
