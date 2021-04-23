using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP
{
    class Card
    {
        // Fields 
        public string Suit { get; private set; }
        public string Value { get; private set; }

        // Constructor
        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;
        }

        // Override to string method to show values of card when printed
        public override string ToString() => $"{Value} of {Suit}";

    }
}