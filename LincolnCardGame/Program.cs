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
                // Make a new game object to be able to play the card game
                Game newGame = new Game();

                // Call the appropriate method to play the game
                newGame.ShowInstructions();
                newGame.PlayGame();
                newGame.EndGame();
            }
        }
    }
}