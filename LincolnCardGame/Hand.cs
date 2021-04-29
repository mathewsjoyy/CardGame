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

        public void Display()
        {
            Console.WriteLine("=== CARDS CURRENTLY IN HAND ===");
            foreach (Card card in AHand) Console.WriteLine(card);
        }

        private void GetAHand(Deck deck)
        {
            if (deck.Cards.Count < 10)
            {
                throw new NotEnoughCardsInDeckException();
            }

            for (int i = 0; i < 10; i++)
            {
                AHand.Add(deck.Deal());
            }
        }
    }
}
