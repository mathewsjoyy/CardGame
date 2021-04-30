using System;

namespace LincolnCardGame
{
    internal abstract class Player : IDisplayable
    {
        public int Score { get; private set; }
        public int ID { get; set; }
        public Hand playerHand { get; private set; }

        public Player(Deck deck)
        {
            playerHand = new Hand(deck);
        }

        // Method to call once a player wins a round
        public void pointWon(int points = 1)
        {
            Score += points;
            Console.WriteLine($"\nPlayer {ID} has won the Round and a Point! their new score is {Score}\n");
        }

        public abstract Tuple<Card, Card> play2Cards();

        public void Display()
        {
            Console.WriteLine($"Player {ID} Details:\n>Player Score : {Score}");
        }
    }
}