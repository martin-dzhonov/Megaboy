using System;

namespace Main
{
#if WINDOWS || XBOX
    static class GameMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Megaboy game = new Megaboy())
            {
                game.Run();
            }
        }

    }
#endif
}

