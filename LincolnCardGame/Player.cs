using System;

namespace LincolnCardGame
{
    internal abstract class Player : IDisplayable
    {
        // Fields
        public int Score { get; private set; }
        public int ID { get; protected set; }
        public Hand playerHand { get; private set; }

        // Constructor
        public Player(Deck deck)
        {
            playerHand = new Hand(deck);
        }

        // Increments the score by 1 or a given integer value
        public void PointWon(int points = 1)
        {
            Score += points;
            Console.WriteLine($"\nPlayer {ID} has won the Round and {points} Point(s)! their new score is {Score}\n");
        }

        // Abstract method to be overridden by child classes
        public abstract Tuple<Card, Card> play2Cards();

        // Draws 1 random card from a the given deck after it is shuffled
        public Card DrawARandomCard(Deck deck)
        {
            deck.Shuffle();
            return deck.Deal();
        }

        // Displays the player details
        public void Display()
        {
            Console.WriteLine($"Player {ID} Details:\n>Player Score : {Score}");
        }

    }
}