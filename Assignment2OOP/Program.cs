using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Call method to start the program
            Deck newDeck = new Deck();
            DrawCards(newDeck);

        }

        // Short program which deals one card from deck at a time and also shuffles the deck
        private static void DrawCards(Deck deck)
        {
            Console.WriteLine("Would you like to shuffle your deck of cards? (type yes or no)");

            // Asks user if they want to shuffle the deck
            while (true)
            {
                string option = Console.ReadLine();
                if (option.ToLower().Trim() == "yes")
                {
                    deck.Shuffle();
                    Console.WriteLine("Deck has been randomly shuffled."); break;
                }
                else if (option.ToLower().Trim() == "no")
                {
                    Console.WriteLine("Your deck is not shuffled."); break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option!");
                }
            }

            Console.Clear();

            // Keeps asking if users wants to draw a card,until deck is empty or user says no
            while (true)
            {
                Console.WriteLine("Would you like to draw a card?" +
                                  "\n> Press ENTER for yes\n> Type 'no' to stop drawing cards");
                string answer = Console.ReadLine();

                if (deck.IsEmpty())
                {
                    Console.WriteLine("No more cards left in your deck!"); break;
                }
                else if (answer.ToLower().Trim() == "")
                {
                    Console.Clear();
                    Console.WriteLine($"> {deck.Deal()}\n");
                }
                else if (answer.ToLower().Trim() == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Sorry I don't know what you want to do.."); break;
                }
            }
        }
    }
}