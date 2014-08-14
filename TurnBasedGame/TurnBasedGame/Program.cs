using System;

namespace TurnBasedGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TurnBasedGame game = new TurnBasedGame())
            {
                game.Run();
            }
        }
    }
#endif
}

