using System;
using System.Collections.Generic;

namespace LincolnCardGame
{
    internal class Hand : IDisplayable
    {
        public List<Card> AHand { get; set; }

        // Assigns 10 cards from deck
        public Hand(Deck deck)
        {
            AHand = new List<Card>();

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    AHand.Add(deck.Deal());
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Seems there isnt enough cards in the deck!");
            }
        }

        public void Display()
        {
            Console.WriteLine("=== CARDS CURRENTLY IN HAND ===");
            foreach (Card card in AHand) Console.WriteLine(card);
        }
    }
}
