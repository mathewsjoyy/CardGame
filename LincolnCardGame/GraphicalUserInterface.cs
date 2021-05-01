using System;

namespace LincolnCardGame
{
    internal class GraphicalUserInterface
    {
        // Used to change the name,foreground and background colours of the console window
        public static void SetGUI(ConsoleColor foreground, ConsoleColor background, string name, bool displayAscii)
        {
            // Check if ascii art is wanted
            if (displayAscii)
            {
                Console.WriteLine("" +
                 " _    _             _         ___             _    ___                \n" +
                 "| |  (_)_ _  __ ___| |_ _    / __|__ _ _ _ __| |  / __|__ _ _ __  ___ \n" +
                @"| |__| | ' \/ _/ _ \ | ' \  | (__/ _` | '_/ _` | | (_ / _` | '  \/ -_)" + "\n" +
                @"|____|_|_||_\__\___/_|_||_|  \___\__,_|_| \__,_|  \___\__,_|_|_|_\___|" + "\n");
            }

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Title = name;
        }
    }
}