namespace LincolnCardGame
{
    // All classes that inherit this interface must have elements in the interface
    internal interface ICardGame
    {
        Deck Deck { get; }
        Player Player1 { get; }
        Player Player2 { get; }

        void ShowInstructions();
        void PlayGame();
        void EndGame();
    }
}
