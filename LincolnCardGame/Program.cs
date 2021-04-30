using System;
using System.Collections.Generic;

namespace LincolnCardGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartGame();
            Console.ReadKey();
        }

        private static void StartGame()
        {
            Console.WriteLine("---==== Welcome to the LINCOLN Card Game ====---");
            Console.WriteLine("> You will play against the computer, and both receive 10 cards." +
                "\n> Both players draw 2 cards,and player with highest total wins hand (and starts next round)" +
                "\n> If totals are the same, continue to next hand. Winning player gets both hands." +
                "\n> Player with highest number of hand wins, wins the game." +
                "\n> If the number of hand wins are the same, draw a random card from the remaining cards - highest wins." +
                "\n> If the final hands are the same value, draw a random card from the remaining cards highest wins the hand." +
                "\n> (Look out for messages starting and ending with '===', these show the state of the game");

            // Make a new deck and shuffle it
            Deck deck = new Deck();
            deck.Shuffle();

            Console.WriteLine("> Press any key to start!");
            Console.ReadKey(); Console.Clear();

            // Begin the rounds of the game
            PlayRounds(deck);
        }


        private static void PlayRounds(Deck deck)
        {
            // Make objects of the 2 players for the game
            Human humanPlayer = new Human(deck);
            Computer compPlayer = new Computer(deck);

            string roundWinner = null;
            int pointsToWin = 1;

            Tuple<Card, Card> human2Cards = null;
            Tuple<Card, Card> comp2Cards = null;

            // Keep playing while players still have cards in their hands
            while (humanPlayer.playerHand.AHand.Count > 1 && compPlayer.playerHand.AHand.Count > 1)
            {
                if (roundWinner == "computer")
                {
                    Console.WriteLine("\n=== Computer will go draw cards first ===");
                    comp2Cards = compPlayer.play2Cards();
                    human2Cards = humanPlayer.play2Cards();
                }
                else
                {
                    Console.WriteLine("\n=== You will draw cards first ===");
                    human2Cards = humanPlayer.play2Cards();
                    comp2Cards = compPlayer.play2Cards();
                }
           
                // Check which player has the higher cards point value added together
                if (human2Cards.Item1 + human2Cards.Item2 > comp2Cards.Item1 + comp2Cards.Item2)
                {
                    humanPlayer.pointWon(pointsToWin);
                    roundWinner = "human";
                    pointsToWin = 1;
                }
                else if (human2Cards.Item1 + human2Cards.Item2 < comp2Cards.Item1 + comp2Cards.Item2)
                {
                    compPlayer.pointWon(pointsToWin);
                    roundWinner = "computer";
                    pointsToWin = 1;
                }
                else
                {
                    pointsToWin += 1;
                    Console.WriteLine($"\n=== Both Player 1 and Player 2 drew the same values of cards! ===" +
                        "=== You will now play again to win {pointsToWin} points! ===");
                }
            }

            Console.Clear();
            Console.WriteLine("==== GAME OVER ====");

            if(humanPlayer.Score == compPlayer.Score)
            {
                humanPlayer.Display();
                compPlayer.Display();
                Console.WriteLine("\n=== Looks like a draw! Both players will draw a random card from the deck ===");
            }
            
        }
    }
}