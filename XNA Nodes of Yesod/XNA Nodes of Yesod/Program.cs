using System;

namespace XNA_Nodes_of_Yesod
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Yesod game = new Yesod())
            {
                game.Run();
            }
        }
    }
}

