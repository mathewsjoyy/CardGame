using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnCardGame
{
    class Human : Player
    {
        public Human(Deck deck) : base(deck)
        {
            ID = 1;
            Console.WriteLine($"Welcome Human Player {ID} you have 10 cards in your hand!");
        }

        // Method to get player to draw 2 cards 
        public override Tuple<Card, Card> play2Cards()
        {
            Card card1; Card card2;

            while (true)
            {
                try
                {
                    playerHand.Display();
                    Console.WriteLine("\nType the number of the card you want to play e.g. typing" +
                    $" '1' would play the card at the top of the list shown above ({playerHand.AHand[1 - 1]}).");
                    var cardPosition1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nType the number of the second card you want to play e.g. typing" +
                    $"'2' would play the card second from the top of the list shown above ({playerHand.AHand[2 - 1]}).");
                    var cardPosition2 = Convert.ToInt32(Console.ReadLine());

                    // Check they havent choosen the same card twice
                    if(cardPosition1 == cardPosition2)
                    {
                        Console.Clear();
                        Console.WriteLine("Sorry you cant choose the same card twice! Try again.\n");
                        continue;
                    }

                    card1 = playerHand.AHand[cardPosition1 - 1];
                    card2 = playerHand.AHand[cardPosition2 - 1];

                    playerHand.AHand.Remove(card1);
                    playerHand.AHand.Remove(card2);

                    Console.Clear();
                    Console.WriteLine($"You have choosen to play {card1} and {card2}");
                    return Tuple.Create(card1, card2);
                }

                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Seems your the card you want isnt avaliable!\n");
                }
            }

        }
    }
}
