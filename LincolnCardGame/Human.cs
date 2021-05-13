using System;

namespace LincolnCardGame
{
    internal class Human : Player
    {
        // Constructor which calls base (Player class) constructor
        public Human(Deck deck, int id = 1) : base(deck, id)
        {
            Console.WriteLine($"Welcome Human (PLAYER {Id}) You Have 10 Cards In Your Hand!");
        }

        // Asks player to select 2 cards they want to draw from a hand
        public override Tuple<Card, Card> Play2Cards()
        {
            if (PlayerHand.IsEmpty())
            {
                Console.WriteLine($"Not enough cards in Player {Id} Hand...");
                return null;
            }

            while (true)
            {
                try
                {
                    PlayerHand.Display();
                    Console.WriteLine("\nType The Number Of The First Card You Want To Play \ne.g. Typing" +
                    $" '1' Would Play The Card At The Top Of The List Shown Above ({PlayerHand.AHand[1 - 1]}).");
                    int cardPosition1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nType The Number Of The Second Card You Want To Play \ne.g. typing" +
                    $" '2' Would Play The Card Second From The Top Of The List Shown Above ({PlayerHand.AHand[2 - 1]}).");
                    int cardPosition2 = Convert.ToInt32(Console.ReadLine());

                    // Check they haven't chosen the same card twice
                    if (cardPosition1 == cardPosition2)
                    {
                        Console.Clear();
                        Console.WriteLine("=== Sorry You Cant Choose The Same Card Twice! Try Again ===\n");
                        continue;
                    }

                    Card chosenCard1 = PlayerHand.AHand[cardPosition1 - 1];
                    Card chosenCard2 = PlayerHand.AHand[cardPosition2 - 1];

                    PlayerHand.AHand.Remove(chosenCard1);
                    PlayerHand.AHand.Remove(chosenCard2);

                    Console.Clear();
                    return Tuple.Create(chosenCard1, chosenCard2);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.Clear();
                    Console.WriteLine("=== Seems The Card You Want Isn't Available! Try Again ===\n");
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("=== That Isn't A Valid Input! Try Again ===\n");
                }
            }
        }
    }
}