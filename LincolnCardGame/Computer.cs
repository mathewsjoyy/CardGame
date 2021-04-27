using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnCardGame
{
    class Computer : Player
    {
        public Computer(Deck deck)
        {
            ID = 2;
            playerHand = new Hand(deck);
            Console.WriteLine($"\nHello I'am player {ID} (computer) who you will try beat!" +
                " I also have 10 cards in my hand!");
        }

        public override Tuple<Card, Card> play2Cards()
        {
            throw new NotImplementedException();
        }
    }
}
