using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Main
{
    static class RectangleHelper
    {
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                r1.Bottom <= r2.Top + (r2.Height / 5) &&
                r1.Right >= r2.Left + r2.Width / 5 &&
                r1.Left <= r2.Right - r2.Width / 5);
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 10) &&
                r1.Top >= r2.Bottom - 1 &&
                r1.Right >= r2.Left + (r2.Width / 10) &&
                r1.Left <= r2.Right - (r2.Width / 10));
        }

        public static bool TouchLeftOff(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                r1.Right >= r2.Left - 2 &&
                r1.Top <= r2.Bottom - (r2.Width / 7) &&
                r1.Bottom >= r2.Top + (r2.Width / 7));
        }

        public static bool TouchRightOff(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                r1.Left <= r2.Right + 2 &&
                r1.Top <= r2.Bottom - (r2.Width / 7) &&
                r1.Bottom >= r2.Top + (r2.Width / 7));
        }
    }
}
