using System;
using System.Collections.Generic;

namespace LincolnCardGame
{
    internal class Deck : IDisplayable
    {
        // Fields
        public List<Card> DeckOfCards { get; private set; }

        // Constructor
        public Deck()
        {
            DeckOfCards = new List<Card>();
            CreateStandardDeck();
        }

        // Fill up the deck when a new object is instansiated
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
            Console.WriteLine($"( Deck Of {DeckOfCards.Count} Cards Created (No Jokers) )");
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
            Console.WriteLine("( The Deck Has Been Shuffled! )\n");
        }

        public Card Deal()
        {
            // Mark sure the deck isnt empty
            if (IsEmpty() == false)
            {
                // Get card at top and return it
                Card topCard = DeckOfCards[DeckOfCards.Count - 1];
                DeckOfCards.Remove(topCard);
                return topCard;
            }
            return null;
        }

        public bool IsEmpty()
        {
            return DeckOfCards.Count < 1;
        }

        public void Display()
        {
            foreach (Card card in DeckOfCards) Console.WriteLine(card);
        }
    }
}