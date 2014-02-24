namespace Main
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public abstract class Enemy : Unit, IUnit, IMovable
    {
        
        public float Timer { get; set; }

        public float Interval { get; set; }

        public int CurrentFrame { get; set; }

        
        public void AnimateRight(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void AnimateLeft(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }


      
    }
}
