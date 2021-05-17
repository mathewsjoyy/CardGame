using System;

namespace LincolnCardGame
{
    internal class Game : ICardGame
    {
        // Fields
        public LogFile Log { get; private set; } = new LogFile();
        public Deck Deck { get; private set; } = new Deck();
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        // Constructor
        public Game()
        {
            // Shuffle the deck
            Deck.Shuffle();

            // Make objects of the 2 players for the game, and give both players a unique ID to keep track off
            Player1 = new Human(Deck);
            Player2 = new Computer(Deck);

            Log.LogMessage("Deck made (and shuffled), Player 1 (Human) and Player 2 (Computer) made");
        }

        // Display instructions for the user about the lincoln card game
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
                " until you both re-veil)" +
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
            while (!Player1.PlayerHand.IsEmpty() && !Player2.PlayerHand.IsEmpty())
            {
                Tuple<Card, Card> player1Cards;
                Tuple<Card, Card> player2Cards;

                // Check who won the previous round and let that player go first
                if (roundWinner == "player2")
                {
                    Console.WriteLine($"=== Round {round} - Player 2 Has Chosen Cards First ===\n");
                    player2Cards = Player2.Play2Cards();
                    player1Cards = Player1.Play2Cards();
                }
                else
                {
                    Console.WriteLine($"=== Round {round} - Player 1 Will Choose Cards First ===\n");
                    player1Cards = Player1.Play2Cards();
                    player2Cards = Player2.Play2Cards();
                }

                Log.LogMessage($"Round {round} - Player 1 Drew {player1Cards.Item1} + {player1Cards.Item2} = {player1Cards.Item1 + player1Cards.Item2}" +
                    $"  - Player 2 Drew {player2Cards.Item1} + {player2Cards.Item2} = {player2Cards.Item1 + player2Cards.Item2}");
                round++;

                Console.WriteLine($"=== Player 2 Has Chosen To Play {player2Cards.Item1} And {player2Cards.Item2}, " +
                    $"Total = {player2Cards.Item1 + player2Cards.Item2} ===");
                Console.WriteLine($"=== Player 1 Has Chosen To Play {player1Cards.Item1} And {player1Cards.Item2}, " +
                    $"Total = {player1Cards.Item1 + player1Cards.Item2} ===");

                // Check which player has the higher cards point value added together
                if (player1Cards.Item1 + player1Cards.Item2 > player2Cards.Item1 + player2Cards.Item2)
                {
                    Player1.PointWon(pointsToWin);
                    roundWinner = "player1";
                    pointsToWin = 1;
                }
                else if (player1Cards.Item1 + player1Cards.Item2 < player2Cards.Item1 + player2Cards.Item2)
                {
                    Player2.PointWon(pointsToWin);
                    roundWinner = "player2";
                    pointsToWin = 1;
                }
                else if (Player1.PlayerHand.AHand.Count < 1 && Player2.PlayerHand.AHand.Count < 1)
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
            Log.LogMessage("Last Round Was A Draw, Both Players Drew Random Card To Find Winner");

            while (true)
            {
                if (Deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty... This Last Round Will Not Count! ===");
                    Log.LogMessage("Empty Deck - No Round Winner\n");
                    break;
                }

                Card player2Card = Player2.DrawARandomCard(Deck);
                Console.WriteLine($"\n=== Player 2 Has Taken A Random Card And Got {player2Card} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card player1Card = Player1.DrawARandomCard(Deck);
                Console.WriteLine($"\n=== Player 1 Has Taken A Random Card And Got {player1Card} ===");

                if (player2Card.PointValue > player1Card.PointValue)
                {
                    Player2.PointWon(pointsToWin);
                    Log.LogMessage($"Player 2 Drew - {player2Card} And Won\n");
                    return;
                }

                if (player2Card.PointValue < player1Card.PointValue)
                {
                    Player1.PointWon(pointsToWin);
                    Log.LogMessage($"Player 1 Drew - {player1Card} And Won\n");
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
            Player1.Display();
            Player2.Display();

            if (Player1.Score == Player2.Score)
            {
                Console.WriteLine("\n=== Looks Like A Draw! Time For A Tie Breaker ===");
                FinalTieBreaker();
                return;
            }

            if (Player1.Score > Player2.Score)
            {
                Console.WriteLine("\n=== Looks Like Player 1 Wins! unlucky player 2 :( ===");
                Log.LogMessage("Found Winner - Player1");
                return;
            }
            Log.LogMessage("Found Winner - Player2");
            Console.WriteLine("\n=== Looks Like Player 2 Wins!, unlucky player 1 :( ===");
        }

        // Decides which player out of 2 wins after a game draw,by getting both players to draw a random
        // card from the existing deck (shuffled again), and player with highest point wins the game.
        private void FinalTieBreaker()
        {
            Console.WriteLine("=== Both Players Will Draw A Random Card From The Deck To Find A Winner ===");

            Log.LogMessage("Game Was A Draw, Both Player Drew Random Card");

            // Both players draw a card from the deck until a winner is found or deck is empty
            while (true)
            {
                if (Deck.IsEmpty())
                {
                    Console.Clear();
                    Console.WriteLine("=== Looks Like The Deck Is Empty! No Winner Today... :( ===");
                    Log.LogMessage("Empty Deck - No Round Winner");
                    break;
                }

                Card player2Card = Player2.DrawARandomCard(Deck);
                Console.WriteLine($"\n=== Player 2 Has Taken A Random Card And Got {player2Card} ===");

                Console.WriteLine("\n=== Press Any Key To Now Draw A Random Card ===");
                Console.ReadKey();

                Card player1Card = Player1.DrawARandomCard(Deck);
                Console.WriteLine($"\n=== You Have Taken A Random Card And Got {player1Card} ===");

                if (player2Card.PointValue > player1Card.PointValue)
                {
                    Console.WriteLine("\n=== Good Job Player 2 You Win! ===");
                    Log.LogMessage($"Player 2 Drew - {player2Card} And Won");
                    return;
                }

                if (player2Card.PointValue < player1Card.PointValue)
                {
                    Console.WriteLine("\n=== Good Job Player 1 You Win! ===");
                    Log.LogMessage($"Player 1 Drew - {player1Card} And Won");
                    return;
                }

                Console.WriteLine("\n=== Its A Draw! Lets Try Again ===");
            }
        }

        // Takes user input to end program or restart
        public void EndGame()
        {
            Console.WriteLine("\n=== Thank You For Playing The Lincoln Card Game! ===\n");

            Console.WriteLine("Overview Of Game Log :"); Log.Display();

            while (true)
            {
                Console.WriteLine("Type 'restart' To Replay The Game, Or 'end' To End The Game : ");
                string option = Console.ReadLine()?.Trim().ToLower();

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