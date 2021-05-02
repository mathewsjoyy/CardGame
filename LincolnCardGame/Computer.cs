using System;

namespace LincolnCardGame
{
    internal class Computer : Player
    {
        // Constructor which calls base (abstract class) constuctor
        public Computer(Deck deck) : base(deck)
        {
            ID = 2;
            Console.WriteLine($"\nHello I'am Computer (PLAYER {ID}) Who You Will Try Beat You!" +
                " I Have 10 Cards In My Hand!\n");
        }

        // Computer picks 2 random cards to play
        public override Tuple<Card, Card> play2Cards()
        {
            Random random = new Random();

            Card card1 = playerHand.AHand[random.Next(playerHand.AHand.Count - 1)];
            playerHand.AHand.Remove(card1);
            Card card2 = playerHand.AHand[random.Next(playerHand.AHand.Count - 1)];
            playerHand.AHand.Remove(card2);

            Console.WriteLine($"\n=== Computer Has Choosen To Play {card1} And {card2} ===");
            return Tuple.Create(card1, card2);
        }
    }
}