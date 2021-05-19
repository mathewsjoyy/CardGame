using System;

namespace LincolnCardGame
{
    internal class NotEnoughCardsException : SystemException
    {
        public NotEnoughCardsException()
        { }

        public NotEnoughCardsException(string message) : base(message)
        { }

        public NotEnoughCardsException(string message, Exception innerException)
        : base(message, innerException)
        { }
    }
}