namespace LincolnCardGame
{
    internal class Card
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
                    return int.Parse(value);
            }
        }

        // Operator overloading to add 2 cards together by their point value
        public static int operator +(Card card1, Card card2) => card1.PointValue + card2.PointValue;

        // Override ToString method to show values of card when printed
        public override string ToString() => $"{Value} of {Suit}";
    }
}