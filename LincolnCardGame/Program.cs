namespace LincolnCardGame
{
    internal class Program
    {
        private static void Main()
        {
            // Make a new game object to be able to play the card game
            Game newGame = new Game();

            // Call the appropriate method to play the game
            newGame.ShowInstructions();
            newGame.StartGame();
            newGame.EndGame();
        }
    }
}