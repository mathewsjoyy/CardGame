using System;
using System.Collections.Generic;

namespace LincolnCardGame
{
    internal class Deck : IDisplayable
    {
        // Fields
        public List<Card> DeckOfCards { get; private set; } = new List<Card>();

        // Constructor
        public Deck()
        {
            CreateStandardDeck();
        }

        // Fill up the deck when a new object is instantiated
        private void CreateStandardDeck()
        {
            List<string> suits = new List<string> { "Spades (♠)", "Hearts (♥)", "Diamonds (♦)", "Clubs (♣)" };
            List<string> values = new List<string> {"2", "3", "4", "5", "6", "7", "8", "9",
                                                     "10", "Jack", "Queen", "King", "Ace"};
            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    DeckOfCards.Add(new Card(suit, value));
                }
            }
            Console.WriteLine($"( Deck Of {DeckOfCards.Count} Cards Created (No Jokers) )\n");
        }

        // Shuffles the deck randomly
        public void Shuffle()
        {
            Random random = new Random();
            int count = DeckOfCards.Count;

            while (count > 1)
            {
                count--;
                int rng = random.Next(count);
                var value = DeckOfCards[rng];
                DeckOfCards[rng] = DeckOfCards[count];
                DeckOfCards[count] = value;
            }
        }

        public Card Deal()
        {
            // Mark sure the deck isn't empty
            if (IsEmpty()) return null;
            // Get card at top and return it
            Card topCard = DeckOfCards[DeckOfCards.Count - 1];
            DeckOfCards.Remove(topCard);
            return topCard;
        }

        // Returns true or false depending on if deck is empty
        public bool IsEmpty() => DeckOfCards.Count < 1;

        public void Display()
        {
            DeckOfCards.ForEach(card => Console.WriteLine(card));
        }
    }
}