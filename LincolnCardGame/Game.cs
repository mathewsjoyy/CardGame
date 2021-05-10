using System;

namespace LincolnCardGame
{
    internal class Game
    {
        // Private Fields
        private Deck _deck;
        private Player _player1;
        private Player _player2;
        private string _Statistics;

        // Constructor
        public Game()
        {
            // Set the console graphical layout
            GraphicalUserInterface.SetGui(ConsoleColor.Green, ConsoleColor.Black, "Lincoln Card Game", true);

            // Make a new deck then shuffle it
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
            Console.WriteLine("1. You will play against the computer, and both have 10 cards." +
                "\n2. Both players draw 2 cards,and player with highest total wins hand (and starts next round)" +
                "\n3. If totals are the same, continue to next hand. Winning player gets both hands." +
                "\n4. Player with highest number of hand wins, wins the game." +
                "\n5. If the number of hand wins are the same, draw a random card from the remaining cards - highest wins." +
                "\n6. If the final hands are the same value, draw a random card from the remaining cards highest wins the hand." +
                "\n> (When you draw your cards you will not be able to see the computers cards and vice versa," +
                " until you both reveil)" +
                "\n> (Look out for messages starting and ending with '===', these show the state of the game)");

            Console.WriteLine("\n> Press Any Key To Continue...");
            Console.ReadKey(); Console.Clear();
        }

        public void PlayGame()
        {
            // Variables to keep track of round winner and points to win per round
            string roundWinner = null;
            int pointsToWin = 1;
            int round = 1;

            // Keep playing while players still have cards in their hands
            while (_player1.PlayerHand.AHand.Count > 0 && _player2.PlayerHand.AHand.Count > 0)
            {
                Tuple<Card, Card> player1_Cards;
                Tuple<Card, Card> player2_Cards;

                // Check who won the previous round and let that player go first
                if (roundWinner == "player2")
                {
                    Console.WriteLine("=== Player 2 Has Chosen Cards First ===");
                    player2_Cards = _player2.Play2Cards();
                    player1_Cards = _player1.Play2Cards();
                }
                else
                {
                    Console.WriteLine("=== Player 1 Will Choose Cards First ===");
                    player1_Cards = _player1.Play2Cards();
                    player2_Cards = _player2.Play2Cards();
                }

                // Logging Game Staistics
                _Statistics += $"Round {round} - Player 1 Drew {player1_Cards.Item1} + {player1_Cards.Item2} = {player1_Cards.Item1 + player1_Cards.Item2}" +
                    $"   - Player 2 Drew {player2_Cards.Item1} + {player2_Cards.Item2} = {player2_Cards.Item1 + player2_Cards.Item2}\n";
                round++;

                Console.WriteLine($"Player 2 Has Chosen To Play {player2_Cards.Item1} And {player2_Cards.Item2}");
                Console.WriteLine($"Player 1 Has Chosen To Play {player1_Cards.Item1} And {player1_Cards.Item2}");


                // Check which player has the higher cards point value added together
                if (player1_Cards.Item1 + player1_Cards.Item2 > player2_Cards.Item1 + player2_Cards.Item2)
                {
                    _player1.PointWon(pointsToWin);
                    roundWinner = "player1";
                    pointsToWin = 1;
                }
                else if (player1_Cards.Item1 + player1_Cards.Item2 < player2_Cards.Item1 + player2_Cards.Item2)
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
                        $"=== You Will Both Draw A Random Card From The Deck To Win {pointsToWin} points ===");

            // Logging game statistics
            _Statistics += "Last Round Was A Draw, Both Players Both Players Drew Random Card To Find Winner\n";

            while (true)
            {
                if (_deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty... This Last Round Will Not Count! ===");
                    // Logging game statistics
                    _Statistics += "Empty Deck - No Round Winner\n";
                    break;
                }

                Card player2Card = _player2.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Player 2 Has Taken A Random Card And Got {player2Card} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card player1Card = _player1.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Player 1 Has Taken A Random Card And Got {player1Card} ===");

                if (player2Card.PointValue > player1Card.PointValue)
                {
                    _player2.PointWon(pointsToWin);
                    // Logging game statistics
                    _Statistics += $"Player 2 Drew - {player2Card} And Won\n";
                    return;
                }

                if (player2Card.PointValue < player1Card.PointValue)
                {
                    _player1.PointWon(pointsToWin);
                    // Logging game statistics
                    _Statistics += $"Player 1 Drew - {player1Card} And Won\n";
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
                // Logging game statistics
                _Statistics += "Found Winner - Player1\n";
                return;
            }
            // Logging game statistics
            _Statistics += "Found Winner - Player2\n";
            Console.WriteLine("\n=== Looks Like Player 2 Wins!, unlucky player 1 :( ===");
        }

        // Decides which player out of 2 wins after a game draw,by getting both players to draw a random
        // card from the existing deck (shuffled again), and player with highest point wins the game.
        private void FinalTieBreaker()
        {
            Console.WriteLine("=== Both Players Will Draw A Random Card From The Deck To Find A Winner ===");

            // Logging game statistics
            _Statistics += "Game Was A Draw, Both Player Drew Random Card\n";

            // Both players draw a card from the deck until a winner is found or deck is empty
            while (true)
            {
                if (_deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty! No Winner Today... :( ===");
                    // Logging game statistics
                    _Statistics += "Empty Deck - No Round Winner\n";
                    break;
                }

                Card player2Card = _player2.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== Player 2 Has Taken A Random Card And Got {player2Card} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card player1Card = _player1.DrawARandomCard(_deck);
                Console.WriteLine($"\n=== You Have Taken A Random Card And Got {player1Card} ===");

                if (player2Card.PointValue > player1Card.PointValue)
                {
                    Console.WriteLine("\n=== Good Job Player 2 You Win! ===");
                    // Logging game statistics
                    _Statistics += $"Player 2 Drew - {player2Card} And Won\n";
                    return;
                }

                if (player2Card.PointValue < player1Card.PointValue)
                {
                    Console.WriteLine("\n=== Good Job Player 1 You Win! ===");
                    // Logging game statistics
                    _Statistics += $"Player 1 Drew - {player1Card} And Won\n";
                    return;
                }

                Console.WriteLine("\n=== Its A Draw! Lets Try Again ===");
            }
        }

        // Takes user input to end program or restart
        public void EndGame()
        {
            Console.WriteLine("\n=== Thank You For Playing The Lincoln Card Game! ===\n");

            Console.WriteLine($"Overview Of Game Statistics : \n{_Statistics}");

            while (true)
            {
                Console.WriteLine("Type 'restart' To Replay The Game, Or 'end' To End The Game : ");
                string option = Console.ReadLine().Trim().ToLower();

                if (option == "end")
                {
                    Environment.Exit(0);
                }
                else if (option == "restart")
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
                Console.WriteLine("Invalid Input Try Again");
            }
        }
    }
}