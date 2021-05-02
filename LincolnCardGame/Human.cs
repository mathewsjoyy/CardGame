using System;

namespace LincolnCardGame
{
    internal class Human : Player
    {
        // Constructor which calls base (Player class) constuctor
        public Human(Deck deck) : base(deck)
        {
            ID = 1;
            Console.WriteLine($"Welcome Human (PLAYER {ID}) You Have 10 Cards In Your Hand!");
        }

        // Asks player to select 2 cards they want to draw from a hand
        public override Tuple<Card, Card> play2Cards()
        {
            Card chosenCard1;
            Card chosenCard2;

            while (true)
            {
                try
                {
                    playerHand.Display();
                    Console.WriteLine("\nType The Number Of The Card You Want To Play \ne.g. Typing" +
                    $" '1' Would Play The Card At The Top Of The List Shown Above ({playerHand.AHand[1 - 1]}).");
                    var cardPosition1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nType The Number Of The Second Card You Want To Play \ne.g. typing" +
                    $"'2' Would Play The Card Second From The Top Of The List Shown Sbove ({playerHand.AHand[2 - 1]}).");
                    var cardPosition2 = Convert.ToInt32(Console.ReadLine());

                    // Check they havent choosen the same card twice
                    if (cardPosition1 == cardPosition2)
                    {
                        Console.Clear();
                        Console.WriteLine("=== Sorry You Cant Choose The Same Card Twice! Try Again ===\n");
                        continue;
                    }

                    chosenCard1 = playerHand.AHand[cardPosition1 - 1];
                    chosenCard2 = playerHand.AHand[cardPosition2 - 1];

                    playerHand.AHand.Remove(chosenCard1);
                    playerHand.AHand.Remove(chosenCard2);

                    Console.Clear();
                    Console.WriteLine($"=== You Have Choosen To Play {chosenCard1} And {chosenCard2} ===");
                    return Tuple.Create(chosenCard1, chosenCard2);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("=== Seems The Card You Want Isnt Avaliable! Try Again\n ===");
                }
            }
        }
    }
}