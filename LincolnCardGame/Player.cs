using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnCardGame
{
    abstract class Player
    {
        public int Score { get; set; }
        public int ID { get; set; }
        public Hand playerHand { get; set; }

        public void pointWon()
        {
            Score += 1;
            Console.WriteLine($"Player {ID} won a point! their new score is {Score}");
        }

        public abstract Tuple<Card, Card> play2Cards();

    }
}
