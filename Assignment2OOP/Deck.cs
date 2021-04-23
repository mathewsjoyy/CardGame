using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP
{
    class Deck
    {
        // Fields
        public List<Card> Cards { get; private set; }

        // Constructor 
        public Deck()
        {
            Cards = new List<Card>();
            CreateStandardDeck();
        }

        // Fill up the deck when a new object is instansiated
        private void CreateStandardDeck()
        {
            List<string> suits = new List<string> { "Spades", "Hearts", "Diamonds", "Clubs", };
            List<string> values = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9",
                                                     "10", "Jack", "Queen", "King", "Ace" };
            foreach(string suit in suits)
            {
                foreach(string value in values)
                {
                    Cards.Add(new Card(suit, value));
                }
            }
            Console.WriteLine($"Deck of {Cards.Count} cards created (No Jokers).\n");
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            int count = Cards.Count;

            while (count > 1)
            {
                count--;
                int rng = rnd.Next(count);
                var value = Cards[rng];
                Cards[rng] = Cards[count];
                Cards[count] = value;
            }
        }

        public string Deal()
        {
            // Mark sure the deck isnt empty
            if (IsEmpty() == false)
            {
                // Get card at top and return it
                Card topCard = Cards[Cards.Count - 1];
                Cards.Remove(topCard);
                return topCard.ToString();
            }
            return "No More Cards In The Deck";
        }

        public bool IsEmpty()
        {
            if (Cards.Count < 1)
                return true;
            else
                return false;
        }
    }
}
