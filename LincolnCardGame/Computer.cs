using System;

namespace LincolnCardGame
{
    internal class Computer : Player
    {
        // Constructor which calls base (abstract class) constructor
        public Computer(Deck deck, int id = 2) : base(deck, id)
        {
            Console.WriteLine($"Hello I'm Computer (PLAYER {Id}) Who You Will Try Beat You!" +
                " I Have 10 Cards In My Hand!\n");
        }

        // Computer picks 2 highest value cards to play then removing them from the Hand
        public override Tuple<Card, Card> Play2Cards()
        {
            if (PlayerHand.IsEmpty())
            {
                Console.WriteLine($"Not enough cards in Player {Id} Hand...");
                return null;
            }

            PlayerHand.SortHand();
            Card card1 = PlayerHand.AHand[0];
            PlayerHand.AHand.Remove(card1);
            Card card2 = PlayerHand.AHand[0];
            PlayerHand.AHand.Remove(card2);

            return Tuple.Create(card1, card2);
        }
    }
}