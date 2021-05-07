using System;

namespace LincolnCardGame
{
    internal class Game
    {
        // Private Fields
        private Deck _deck;
        private Player _player1;
        private Player _player2;

        // Constructor
        public Game()
        {
            // Set the console graphical layout
            GraphicalUserInterface.SetGui(ConsoleColor.Green, ConsoleColor.Black, "Lincoln Card Game", true);

            // Make a new deck and shuffle it
            _deck = new Deck();
            _deck.Shuffle();

            // Make objects of the 2 players for the game, and give both players a unqiue ID to keep track off
            _player1 = new Human(_deck, 1);
            _player2 = new Computer(_deck, 2);
        }

        // Display instructions for the user to know how the game actually works
        public void ShowInstructions()
        {
            Console.WriteLine("---==== Instructions For The LINCOLN Card Game ====---");
            Console.WriteLine("1. You will play against the computer, and both receive 10 cards." +
                "\n2. Both players draw 2 cards,and player with highest total wins hand (and starts next round)" +
                "\n3. If totals are the same, continue to next hand. Winning player gets both hands." +
                "\n4. Player with highest number of hand wins, wins the game." +
                "\n5. If the number of hand wins are the same, draw a random card from the remaining cards - highest wins." +
                "\n6. If the final hands are the same value, draw a random card from the remaining cards highest wins the hand." +
                "\n> (When you draw your cards you will not be able to see the computers cards and vice versa," +
                " until you both reveil)" +
                "\n> (Look out for messages starting and ending with '===', these show the state of the game");

            Console.WriteLine("\n> Press Any Key To Continue...");
            Console.ReadKey(); Console.Clear();
        }

        public void PlayGame()
        {
            // Variables to keep track of round winner and points to win per round
            string roundWinner = null;
            int pointsToWin = 1;

            // Keep playing while players still have cards in their hands
            while (_player1.PlayerHand.AHand.Count > 0 && _player2.PlayerHand.AHand.Count > 0)
            {
                Tuple<Card, Card> player1_2Cards;
                Tuple<Card, Card> player2_Cards;

                // Check who won the previous round and let that player go first
                if (roundWinner == "player2")
                {
                    Console.WriteLine("=== Player 2 Has Chosen Cards First ===");
                    player2_Cards = _player2.Play2Cards();
                    player1_2Cards = _player1.Play2Cards();
                }
                else
                {
                    Console.WriteLine("=== Player 1 Will Choose Cards First ===");
                    player1_2Cards = _player1.Play2Cards();
                    player2_Cards = _player2.Play2Cards();
                }

                Console.WriteLine($"Player 2 Has Chosen To Play {player2_Cards.Item1} And {player2_Cards.Item2}");
                Console.WriteLine($"Player 1 Has Chosen To Play {player1_2Cards.Item1} And {player1_2Cards.Item2}");


                // Check which player has the higher cards point value added together
                if (player1_2Cards.Item1 + player1_2Cards.Item2 > player2_Cards.Item1 + player2_Cards.Item2)
                {
                    _player1.PointWon(pointsToWin);
                    roundWinner = "player1";
                    pointsToWin = 1;
                }
                else if (player1_2Cards.Item1 + player1_2Cards.Item2 < player2_Cards.Item1 + player2_Cards.Item2)
                {
                    _player2.PointWon(pointsToWin);
                    roundWinner = "player2";
                    pointsToWin = 1;
                }
                else if (_player1.PlayerHand.AHand.Count < 1 && _player2.PlayerHand.AHand.Count < 1)
                {
                    pointsToWin += 1;
                    LastRoundDraw(pointsToWin);
                }
                else
                {
                    pointsToWin += 1;
                    Console.WriteLine("\n=== Both Player 1 And Player 2 Drew The Same Values Of Cards! ===\n" +
                                      $"=== You Will Now Play Again To Win {pointsToWin} Points! ===");
                }
            }

            FindWinner();
        }

        // If the final hands are the same value, draw a random card from the remaining cards in the deck, highest
        // wins the hand.
        private void LastRoundDraw(int pointsToWin = 1)
        {
            Console.WriteLine("\n=== Both Players Had Same Value Of Cards On The Last Round! ===\n" +
                        $"=== You Will Both Draw A Random Card From The Deck To Win {pointsToWin} ===");

            while (true)
            {
                if (_deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty... This Last Round Will Not Count! ===");
                    break;
                }

                Card compCard = _player2.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Player 2 Has Taken A Random Card And Got {compCard} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card humanCard = _player1.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Player 1 Has Taken A Random Card And Got {humanCard} ===");

                if (compCard.PointValue > humanCard.PointValue)
                {
                    _player2.PointWon(pointsToWin);
                    return;
                }

                if (compCard.PointValue < humanCard.PointValue)
                {
                    _player1.PointWon(pointsToWin);
                    return;
                }

                Console.WriteLine("\n=== Its A Draw! Lets Try Again ===");
            }
        }

        // Find play with highest score, if its a draw call for a tiebreaker
        private void FindWinner()
        {
            Console.WriteLine("\n==== GAME OVER ====");

            // Display both players stats (scores / hands won)
            _player1.Display();
            _player2.Display();

            if (_player1.Score == _player2.Score)
            {
                Console.WriteLine("\n=== Looks Like A Draw! Time For A Tie Breaker ===");
                FinalTieBreaker();
                return;
            }

            if (_player1.Score > _player2.Score)
            {
                Console.WriteLine("\n=== Looks Like Player 1 Wins! unlucky player 2 :( ===");
                return;
            }

            Console.WriteLine("\n=== Looks Like Player 2 Wins!, unlucky player 1 :( ===");
        }

        // Decides which player out of 2 wins after a game draw,by getting both players to draw a random
        // card from the existing deck (shuffled again), and player with highest point wins the game.
        private void FinalTieBreaker()
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

                Card player2 = _player2.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Player 2 Has Taken A Random Card And Got {player2} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card player1 = _player1.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== You Have Taken A Random Card And Got {player1} ===");

                if (player2.PointValue > player1.PointValue)
                {
                    Console.WriteLine("\n=== Good Job Player 2 You Win! ===");
                    return;
                }

                if (player2.PointValue < player1.PointValue)
                {
                    Console.WriteLine("\n=== Good Job Player 1 You Win! ===");
                    return;
                }

                Console.WriteLine("\n=== Its A Draw! Lets Try Again ===");
            }
        }

        // Gives an exit message and ends the program
        public void EndGame()
        {
            Console.WriteLine("\n=== Thank You For Playing The Lincoln Card Game! ===\n" +
                "\n\t(press any key to exit!)");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}