using Game1;
using System;

namespace Projet_03
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (var game = new Game1())

                game.Run();
            
        }
    }
#endif
}
