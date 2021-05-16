using System;
using System.Collections.Generic;
using System.Linq;

namespace LincolnCardGame
{
    internal class Hand : IDisplayable
    {
        // Fields
        public List<Card> AHand { get; private set; }

        // Creates a new hand and assigns 10 cards from deck
        public Hand(Deck deck)
        {
            AHand = new List<Card>();

            try
            {
                GetAHand(deck);
            }
            catch (NotEnoughCardsException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Any Key To Exit The Game! And Check Your Deck.");
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }

        // Gets 10 cards from a given deck
        private void GetAHand(Deck deck)
        {
            if (deck.DeckOfCards.Count < 10)
            {
                throw new NotEnoughCardsException();
            }

            for (int i = 0; i < 10; i++)
            {
                AHand.Add(deck.Deal());
            }
        }

        // Display every card in hand
        public void Display()
        {
            Console.WriteLine("=== Cards Currently In Your Hand ===");
            AHand.ForEach(card => Console.WriteLine(card));
        }

        // Returns true or false depending on if hand is empty
        public bool IsEmpty() => AHand.Count < 1;

        // Sorts hand by its point value
        public void SortHand()
        {
            AHand = AHand.OrderByDescending(card => card.PointValue).ToList();
        }
    }
}