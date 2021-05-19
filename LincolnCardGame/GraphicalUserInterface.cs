using System;

namespace LincolnCardGame
{
    internal class GraphicalUserInterface
    {
        // Used to change the name,foreground,background colors and the size of the console window
        public static void SetGui(ConsoleColor foreground, ConsoleColor background, string name,
            int height = 35, int width = 145)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Title = name;
            Console.SetWindowSize(160, 35);
        }
    }
}