using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnCardGame
{
    class Game
    {
        // Fields
        private  Deck deck { get; set; }
        private  Human humanPlayer { get; set; }
        private  Computer compPlayer { get; set; }

        // Constructor
        public Game()
        {
            // Set the console graphical layout
            GraphicalUserInterface.SetGUI(ConsoleColor.Green, ConsoleColor.Black, "Lincoln Card Game", true);

            // Display game instructions
            ShowInstructions();

            // Make a new deck and shuffle it
            deck = new Deck();
            deck.Shuffle();

            // Make objects of the 2 players for the game
            humanPlayer = new Human(deck);
            compPlayer = new Computer(deck);
        }

        public void ShowInstructions()
        {
            // Display instructions for the user to know how the game actually works
            Console.WriteLine("---==== Instructions For The LINCOLN Card Game ====---");
            Console.WriteLine("1. You will play against the computer, and both receive 10 cards." +
                "\n2. Both players draw 2 cards,and player with highest total wins hand (and starts next round)" +
                "\n3. If totals are the same, continue to next hand. Winning player gets both hands." +
                "\n4. Player with highest number of hand wins, wins the game." +
                "\n5. If the number of hand wins are the same, draw a random card from the remaining cards - highest wins." +
                "\n6. If the final hands are the same value, draw a random card from the remaining cards highest wins the hand." +
                "\n> (Look out for messages starting and ending with '===', these show the state of the game");

            while (true)
            {
                Console.WriteLine("\n> Press ENTER Key To Continue or ESC Key to End The Game");
                ConsoleKeyInfo option = Console.ReadKey();
                Console.Clear();

                if (option.Key == ConsoleKey.Escape)
                {
                    EndGame();
                }
                else if (option.Key == ConsoleKey.Enter)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("\n> Make sure you entered a valid key on your keyboard");
                }
            }
        }

        public void StartGame()
        {
            // Variables to keep track of round winner and points to win per round
            string roundWinner = null;
            int pointsToWin = 1;

            // Keep playing while players still have cards in their hands
            while (humanPlayer.playerHand.AHand.Count > 1 && compPlayer.playerHand.AHand.Count > 1)
            {
                Tuple<Card, Card> human2Cards;
                Tuple<Card, Card> comp2Cards;

                if (roundWinner == "computer")
                {
                    Console.WriteLine("=== Computer Will Draw Cards First ===");
                    comp2Cards = compPlayer.play2Cards();
                    human2Cards = humanPlayer.play2Cards();
                }
                else
                {
                    Console.WriteLine("=== You Will Draw Cards First ===");
                    human2Cards = humanPlayer.play2Cards();
                    comp2Cards = compPlayer.play2Cards();
                }

                // Check which player has the higher cards point value added together
                if (human2Cards.Item1 + human2Cards.Item2 > comp2Cards.Item1 + comp2Cards.Item2)
                {
                    humanPlayer.PointWon(pointsToWin);
                    roundWinner = "human";
                    pointsToWin = 1;
                }
                else if (human2Cards.Item1 + human2Cards.Item2 < comp2Cards.Item1 + comp2Cards.Item2)
                {
                    compPlayer.PointWon(pointsToWin);
                    roundWinner = "computer";
                    pointsToWin = 1;
                }
                else
                {
                    pointsToWin += 1;
                    Console.WriteLine($"\n=== Both Player 1 And Player 2 Drew The Same Values Of Cards! ===" +
                        $"=== You Will Now Play Again To Win {pointsToWin} Points! ===");
                }
            }

            Console.WriteLine("\n==== GAME OVER ====");

            // Display both players stats (scores / hands won)
            humanPlayer.Display();
            compPlayer.Display();

            if (humanPlayer.Score == compPlayer.Score)
            {
                Console.WriteLine("\n=== Looks Like A Draw! ===");
                TieBreaker(deck, humanPlayer, compPlayer);
            }
            else if (humanPlayer.Score > compPlayer.Score)
            {
                Console.WriteLine("\n=== Looks Like You Win, Good Job! ===");
            }
            else
            {
                Console.WriteLine("\n=== Looks Like The Computer Wins, Better Luck Next Time! ===");
            }
        }

        // Decides which player out of 2 wins after a game draw,by getting both players to draw a random
        // card from the existing deck (shuffled again), and player with highest point wins the game.
        public void TieBreaker(Deck deck, Player humanPlayer, Player compPlayer)
        {
            Console.WriteLine("=== Both Players Will Draw A Random Card From The Deck To Find A Winner ===");

            bool winnerFound = false;

            // Both players draw a card from the deck until a winner is found or deck is empty
            while (!winnerFound)
            {
                if (deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty! No Winner Today... :( ===");
                    EndGame();
                }

                Card compCard = compPlayer.DrawARandomCard(deck);
                Console.WriteLine($"\n=== Computer Has Taken A Random Card And Got {compCard} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card humanCard = humanPlayer.DrawARandomCard(deck);
                Console.WriteLine($"\n=== You Have Taken A Random Card And Got {humanCard} ===");

                if (compCard.PointValue > humanCard.PointValue)
                {
                    Console.WriteLine("\n=== Looks Like The Computer Wins, Better Luck Next Time! ===");
                    winnerFound = true;
                }
                else if (compCard.PointValue < humanCard.PointValue)
                {
                    Console.WriteLine("\n=== Looks Like You Win, Good Job! ===");
                    winnerFound = true;
                }
                else
                {
                    Console.WriteLine("\n=== Its A Draw! Lets Try Again ===");
                }
            }
        }

        // Gives an exit message and ends the program
        public void EndGame()
        {
            Console.WriteLine("\n== Thank You For Playing The Lincoln Card Game! ===\n" +
                "\n\t(press any key to exit!)");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
