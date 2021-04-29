using System;

namespace LincolnCardGame
{
    internal abstract class Player : IDisplayable
    {
        public int Score { get; private set; }
        public int ID { get; set; }
        public Hand playerHand { get; private set; }

        public Player(Deck deck)
        {
            playerHand = new Hand(deck);
        }

        // 
        public void pointWon()
        {
            Score += 1;
            Console.WriteLine($"Player {ID} won a point! their new score is {Score}");
        }

        public abstract Tuple<Card, Card> play2Cards();

        public void Display()
        {
            Console.WriteLine($"Player Details - Player ID : {ID}\nPlayer Score : {Score}");
        }

    }
}
