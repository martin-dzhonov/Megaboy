namespace Main.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public interface IProjectile
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        bool IsVisible { get; set; }
    }
}
