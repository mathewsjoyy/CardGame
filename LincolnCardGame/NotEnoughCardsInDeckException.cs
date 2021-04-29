using System;

namespace LincolnCardGame
{
    internal class NotEnoughCardsInDeckException : SystemException
    {
        public NotEnoughCardsInDeckException()
            : base("Not enough cards in your deck!")
        {
        }
    }
}
