using System;

namespace LincolnCardGame
{
    internal abstract class Player : IDisplayable
    {
        // Fields
        public int Score { get; set; }
        protected int Id { get; set; }
        public Hand PlayerHand { get; set; }

        // Constructor
        protected Player(Deck deck, int id)
        {
            Id = id;
            PlayerHand = new Hand(deck);
        }

        // Increments the score by 1 or a given integer value
        public void PointWon(int points = 1)
        {
            Score += points;
            Console.WriteLine($"\n=== Player {Id} Has Won The Round And {points} Point(s)! Their New Score Is {Score} ===\n");
        }

        // Abstract method to be overridden by child classes to provide functionality to play 2 cards
        public abstract Tuple<Card, Card> Play2Cards();

        // Draws 1 random card from a the given deck after it is shuffled
        public Card DrawARandomCard(Deck deck)
        {
            deck.Shuffle();
            return deck.Deal();
        }

        // Displays the players key details
        public void Display() => Console.WriteLine($"Player {Id} Details:\n>Score : {Score}");
    }
}