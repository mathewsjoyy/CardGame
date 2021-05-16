using System;

namespace LincolnCardGame
{
    internal class Program
    {
        private static void Main()
        {
            // Set the console graphical layout
            GraphicalUserInterface.SetGui(ConsoleColor.Green, ConsoleColor.Black, "Lincoln Card Game");

            // Loop to keep the game running if user wants to play again
            while (true)
            {
                Game newGame = new Game();

                newGame.ShowInstructions();
                newGame.PlayGame();
                newGame.EndGame();
            }
        }
    }
}