namespace LincolnCardGame
{
    internal class Card
    {
        // Fields
        public string Suit { get; private set; }

        public string Value { get; private set; }
        public int pointValue { get; private set; }

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
                pointValue = int.Parse(value);
            }
            else
            {
                switch (value)
                {
                    case "Jack":
                        pointValue = 11;
                        break;

                    case "Queen":
                        pointValue = 12;
                        break;

                    case "King":
                        pointValue = 13;
                        break;

                    case "Ace":
                        pointValue = 14;
                        break;
                }
            }
        }

        // Operator overloading to add 2 cards together by their point value
        public static int operator +(Card card1, Card card2) => card1.pointValue + card2.pointValue;

        // Override to string method to show values of card when printed
        public override string ToString() => $"{Value} of {Suit}";
    }
}