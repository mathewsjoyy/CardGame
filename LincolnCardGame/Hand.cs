using System;
using System.Collections.Generic;
using System.Linq;

namespace LincolnCardGame
{
    internal class Hand : IDisplayable
    {
        // Fields
        public List<Card> AHand { get; private set; } = new List<Card>();

        // Creates a new hand and assigns 10 cards from deck
        public Hand(Deck deck)
        {
            try
            {
                GetAHand(deck);
            }
            catch (NotEnoughCardsException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press Any Key To Exit The Game! And Check Your Deck: ");
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }

        // Gets 10 cards from a given deck
        private void GetAHand(Deck deck)
        {
            if (deck.DeckOfCards.Count < 10)
            {
                throw new NotEnoughCardsException("Not Enough Cards In Deck");
            }

            for (int i = 0; i < 10; i++)
            {
                AHand.Add(deck.Deal());
            }
        }

        // Returns true or false depending on if hand is empty
        public bool IsEmpty() => AHand.Count < 1;

        // Sorts hand by its point value
        public void SortHand()
        {
            AHand = AHand.OrderByDescending(card => card.PointValue).ToList();
        }

        // Display every card in hand
        public void Display()
        {
            Console.WriteLine("=== Cards Currently In Hand ===");
            int cardCounter = 1;
            foreach (Card card in AHand)
            {
                Console.WriteLine($"[{cardCounter}] {card}");
                cardCounter++;
            }
        }
    }
}