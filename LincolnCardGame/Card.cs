namespace LincolnCardGame
{
    internal class Card
    {
        // Fields
        public string Suit { get; }
        public string Value { get; }
        public int PointValue { get; private set; }

        // Constructor
        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;

            SetCardPointValue(value);
        }

        // Gets a cards point value and assigns it to the pointValue field
        private void SetCardPointValue(string value)
        {
            if (int.TryParse(value, out _))
            {
                PointValue = int.Parse(value);
            }
            else
            {
                switch (value)
                {
                    case "Jack":
                        PointValue = 11;
                        break;

                    case "Queen":
                        PointValue = 12;
                        break;

                    case "King":
                        PointValue = 13;
                        break;

                    case "Ace":
                        PointValue = 14;
                        break;
                }
            }
        }

        // Operator overloading to add 2 cards together by their point value
        public static int operator +(Card card1, Card card2) => card1.PointValue + card2.PointValue;

        // Override ToString method to show values of card when printed
        public override string ToString() => $"{Value} of {Suit}";
    }
}