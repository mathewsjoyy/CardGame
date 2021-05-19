using System;

namespace LincolnCardGame
{
    internal class Card : IEquatable<Card>
    {
        // Fields
        public string Suit { get; private set; }
        public string Value { get; private set; }
        public int PointValue { get; private set; }

        // Constructor
        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;
            PointValue = SetCardPointValue(value);
        }

        // Gets a cards point value and assigns it to the pointValue field
        private int SetCardPointValue(string value)
        {
            if (int.TryParse(value, out _))
            {
                return int.Parse(value);
            }
            else
            {
                switch (value)
                {
                    case "Jack":
                        return 11;

                    case "Queen":
                        return 12;

                    case "King":
                        return 13;

                    case "Ace":
                        return 14;

                    default:
                        return 0;
                }
            }
        }

        // Operator overloading to compare cards by their PointValue
        public static int operator +(Card card1, Card card2) => card1.PointValue + card2.PointValue;
        public static int operator -(Card card1, Card card2) => card1.PointValue - card2.PointValue;
        public static bool operator >(Card card1, Card card2) => card1.PointValue > card2.PointValue;
        public static bool operator <(Card card1, Card card2) => card1.PointValue < card2.PointValue;

        // Compare 2 cards
        public bool Equals(Card other)
        {
            return other != null && Suit == other.Suit && Value == other.Value;
        }

        // Override ToString method to show values of card when printed
        public override string ToString() => $"{Value} of {Suit}";
    }
}