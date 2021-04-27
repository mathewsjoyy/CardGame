using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnCardGame
{
    class Card
    {
        // Fields 
        public string Suit { get; private set; }
        public string Value { get; private set; }
        public int pointValue { get; set; }

        // Constructor
        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;

            getCardPointValue(value);
        }

        // Override to string method to show values of card when printed
        public override string ToString() => $"{Value} of {Suit}";

        // Gets card point value
        private void getCardPointValue(string value)
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

    }
}