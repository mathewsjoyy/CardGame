using System;

namespace LincolnCardGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Make a new game object to be able to play the card game
            Game newGame = new Game();

            // Call the appropriate methods to play the game
            newGame.StartGame();
            newGame.EndGame();
        }
    }
}