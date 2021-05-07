using System;

namespace LincolnCardGame
{
    internal class NotEnoughCardsException : SystemException
    {
        public NotEnoughCardsException()
            : base("Not enough cards.")
        {
        }
    }
}