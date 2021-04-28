using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            playGame();

            Console.ReadKey();
        }
        private static void playGame()
        {
            Console.WriteLine("---==== Welcome to the LINCOLN Card Game ====---");
            Console.WriteLine("> You will play against the computer, and both receive 10 cards." +
                "\n> Both players draw 2 cards,and player with highest total wins hand (and starts next round)" +
                "\n> If totals are the same, continue to next hand. Winning player gets both hands." +
                "\n> Player with highest number of hand wins, wins the game." +
                "\n> If the number of hand wins are the same, draw a random card from the remaining cards - highest wins." +
                "\n> If the final hands are the same value, draw a random card from the remaining cards highest wins the hand.");

            // Make a new deck and shuffle it
            Deck deck = new Deck();
            deck.Shuffle();

            // Make objects of the 2 players for the game
            Human humanPlayer = new Human(deck);
            Computer compPlayer = new Computer(deck);

            Console.WriteLine("\n> Press any key to start!");
            Console.ReadKey(); Console.Clear();

            var humanPlayedCards = humanPlayer.play2Cards();

            var compPlayedCards = compPlayer.play2Cards();


        }
    }
}