namespace LincolnCardGame
{
    internal class Program
    {
        private static void Main()
        {
            // Make a new game
            Game newGame = new Game();

            // Loop to keep the game running if user wants to play again
            while (true)
            {
                newGame.ShowInstructions();
                newGame.PlayGame();
                newGame.EndGame();
            }
        }
    }
}