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
            using (Engine engine = new Engine())
            {
                engine.Run();
            }
        }

    }
#endif
}

