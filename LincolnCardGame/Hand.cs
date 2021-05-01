using System;
using System.Collections.Generic;

namespace LincolnCardGame
{
    internal class Hand : IDisplayable
    {
        // Fields
        public List<Card> AHand { get; private set; }

        // Assigns 10 cards from deck
        public Hand(Deck deck)
        {
            AHand = new List<Card>();

            try
            {
                GetAHand(deck);
            }
            catch (NotEnoughCardsInDeckException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit the game! and check your deck.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        // Gets 10 cards from a given deck
        private void GetAHand(Deck deck)
        {
            if (deck.DeckOfCards.Count < 10)
            {
                throw new NotEnoughCardsInDeckException();
            }

            for (int i = 0; i < 10; i++)
            {
                AHand.Add(deck.Deal());
            }
        }

        public void Display()
        {
            Console.WriteLine("\n=== Cards currently in your hand ===");
            foreach (Card card in AHand) Console.WriteLine(card);
        }
    }
}