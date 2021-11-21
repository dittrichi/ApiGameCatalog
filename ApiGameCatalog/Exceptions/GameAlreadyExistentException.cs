using System;

namespace ApiGameCatalog.Exceptions
{
    public class GameAlreadyExistentException : Exception
    {
        public GameAlreadyExistentException()
            : base("This game already exists")
        { }
    }
}
