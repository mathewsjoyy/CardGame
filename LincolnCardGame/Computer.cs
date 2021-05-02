using System;

namespace LincolnCardGame
{
    internal class Computer : Player
    {
        // Constructor which calls base (abstract class) constructor
        public Computer(Deck deck) : base(deck)
        {
            Id = 2;
            Console.WriteLine($"\nHello I'm Computer (PLAYER {Id}) Who You Will Try Beat You!" +
                " I Have 10 Cards In My Hand!\n");
        }

        // Computer picks 2 random cards to play
        public override Tuple<Card, Card> Play2Cards()
        {
            Random random = new Random();

            Card card1 = PlayerHand.AHand[random.Next(PlayerHand.AHand.Count - 1)];
            PlayerHand.AHand.Remove(card1);
            Card card2 = PlayerHand.AHand[random.Next(PlayerHand.AHand.Count - 1)];
            PlayerHand.AHand.Remove(card2);

            Console.WriteLine($"\n=== Computer Has Chosen To Play {card1} And {card2} ===");
            return Tuple.Create(card1, card2);
        }
    }
}