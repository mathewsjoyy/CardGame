using System;

namespace LincolnCardGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Call the methods to play the game
            ShowInstructions();
            StartGame();
            EndGame();
        }

        private static void ShowInstructions()
        {
            GraphicalUserInterface.SetGUI(ConsoleColor.Green, ConsoleColor.Black, "Lincoln Card Game", true);

            Console.WriteLine("---==== Welcome to the LINCOLN Card Game ====---");
            Console.WriteLine("1. You will play against the computer, and both receive 10 cards." +
                "\n2. Both players draw 2 cards,and player with highest total wins hand (and starts next round)" +
                "\n3. If totals are the same, continue to next hand. Winning player gets both hands." +
                "\n4. Player with highest number of hand wins, wins the game." +
                "\n5. If the number of hand wins are the same, draw a random card from the remaining cards - highest wins." +
                "\n6. If the final hands are the same value, draw a random card from the remaining cards highest wins the hand." +
                "\n> (Look out for messages starting and ending with '===', these show the state of the game");

            while (true)
            {
                Console.WriteLine("\n> Press ENTER To Start or ESC to End The Game");
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

        private static void StartGame()
        {
            // Make a new deck and shuffle it
            Deck deck = new Deck();
            deck.Shuffle();

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
                    Console.WriteLine("=== Computer Will Go Draw Cards First ===");
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

        private static void TieBreaker(Deck deck, Player humanPlayer, Player compPlayer)
        {
            Console.WriteLine("=== Both Players Will Draw A Random Card From The Deck To Find A Winner ===");

            bool winnerFound = false;
            deck.Shuffle();

            while (winnerFound == false && deck.IsEmpty() == false)
            {
                Card compCard = deck.Deal();
                Console.WriteLine($"\n=== Computer Has Taken A Random Card And Got {compCard} ===");
                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Card humanCard = deck.Deal();
                Console.WriteLine($"\n=== You Have Taken A Random Card And Got {humanCard} ===");

                if (compCard.pointValue > humanCard.pointValue)
                {
                    Console.WriteLine("\n=== Looks Like The Computer Wins, Better Luck Next Time! ===");
                    winnerFound = true;
                }
                else if (compCard.pointValue < humanCard.pointValue)
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
        private static void EndGame()
        {
            Console.WriteLine("\n== Thank You For Playing The Lincoln Card Game! ===\n" +
                "\n\t(press any key to exit!)");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}