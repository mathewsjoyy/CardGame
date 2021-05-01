using System;

namespace LincolnCardGame
{
    internal class Computer : Player
    {
        // Constructor which calls base (abstract class) constuctor
        public Computer(Deck deck) : base(deck)
        {
            ID = 2;
            Console.WriteLine($"\nHello I'am Computer (PLAYER {ID}) who you will try beat!" +
                " I also have 10 cards in my hand!\n");
        }

        // Computer picks 2 random cards to play
        public override Tuple<Card, Card> play2Cards()
        {
            Random random = new Random();

            Card card1 = playerHand.AHand[random.Next(playerHand.AHand.Count - 1)];
            playerHand.AHand.Remove(card1);
            Card card2 = playerHand.AHand[random.Next(playerHand.AHand.Count - 1)];
            playerHand.AHand.Remove(card2);

            Console.WriteLine($"\n=== Computer has choosen to play {card1} and {card2} ===");
            return Tuple.Create(card1, card2);
        }
    }
}