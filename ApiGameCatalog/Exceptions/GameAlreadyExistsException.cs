using System;

namespace ApiGameCatalog.Exceptions
{
    public class GameAlreadyExistsException : Exception
    {
        public GameAlreadyExistsException()
            : base("This game already exists")
        { }
    }
}
