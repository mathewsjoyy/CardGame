using System;

namespace LincolnCardGame
{
    internal class Game
    {
        // Fields
        private Deck _deck;
        private Human _humanPlayer;
        private Computer _compPlayer;

        // Constructor
        public Game()
        {
            // Set the console graphical layout
            GraphicalUserInterface.SetGui(ConsoleColor.Green, ConsoleColor.Black, "Lincoln Card Game", true);
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

            Console.WriteLine("\n> Press Any Key To Continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public void StartGame()
        {
            // Make a new deck and shuffle it
            _deck = new Deck();
            _deck.Shuffle();

            // Make objects of the 2 players for the game
            _humanPlayer = new Human(_deck);
            _compPlayer = new Computer(_deck);

            // Variables to keep track of round winner and points to win per round
            string roundWinner = null;
            int pointsToWin = 1;

            // Keep playing while players still have cards in their hands
            while (_humanPlayer.PlayerHand.AHand.Count > 1 && _compPlayer.PlayerHand.AHand.Count > 1)
            {
                Tuple<Card, Card> human2Cards;
                Tuple<Card, Card> comp2Cards;

                if (roundWinner == "computer")
                {
                    Console.WriteLine("=== Computer Will Draw Cards First ===");
                    comp2Cards = _compPlayer.Play2Cards();
                    human2Cards = _humanPlayer.Play2Cards();
                }
                else
                {
                    Console.WriteLine("=== You Will Draw Cards First ===");
                    human2Cards = _humanPlayer.Play2Cards();
                    comp2Cards = _compPlayer.Play2Cards();
                }

                // Check which player has the higher cards point value added together
                if (human2Cards.Item1 + human2Cards.Item2 > comp2Cards.Item1 + comp2Cards.Item2)
                {
                    _humanPlayer.PointWon(pointsToWin);
                    roundWinner = "human";
                    pointsToWin = 1;
                }
                else if (human2Cards.Item1 + human2Cards.Item2 < comp2Cards.Item1 + comp2Cards.Item2)
                {
                    _compPlayer.PointWon(pointsToWin);
                    roundWinner = "computer";
                    pointsToWin = 1;
                }
                else
                {
                    pointsToWin += 1;
                    Console.WriteLine("\n=== Both Player 1 And Player 2 Drew The Same Values Of Cards! ===" +
                        $"=== You Will Now Play Again To Win {pointsToWin} Points! ===");
                }
            }

            Console.WriteLine("\n==== GAME OVER ====");

            // Display both players stats (scores / hands won)
            _humanPlayer.Display();
            _compPlayer.Display();

            if (_humanPlayer.Score == _compPlayer.Score)
            {
                Console.WriteLine("\n=== Looks Like A Draw! ===");
                TieBreaker();
            }
            else if (_humanPlayer.Score > _compPlayer.Score)
            {
                Console.WriteLine("\n=== Looks Like You Win, Good Job! ===");
            }

            Console.WriteLine("\n=== Looks Like The Computer Wins, Better Luck Next Time! ===");
        }

        // Decides which player out of 2 wins after a game draw,by getting both players to draw a random
        // card from the existing deck (shuffled again), and player with highest point wins the game.
        private void TieBreaker()
        {
            Console.WriteLine("=== Both Players Will Draw A Random Card From The Deck To Find A Winner ===");

            // Both players draw a card from the deck until a winner is found or deck is empty
            while (true)
            {
                if (_deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty! No Winner Today... :( ===");
                    break;
                }

                Card compCard = _compPlayer.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Computer Has Taken A Random Card And Got {compCard} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card humanCard = _humanPlayer.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== You Have Taken A Random Card And Got {humanCard} ===");

                if (compCard.PointValue > humanCard.PointValue)
                {
                    Console.WriteLine("\n=== Looks Like The Computer Wins, Better Luck Next Time! ===");
                    return;
                }

                if (compCard.PointValue < humanCard.PointValue)
                {
                    Console.WriteLine("\n=== Looks Like You Win, Good Job! ===");
                    return;
                }

                Console.WriteLine("\n=== Its A Draw! Lets Try Again ===");
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