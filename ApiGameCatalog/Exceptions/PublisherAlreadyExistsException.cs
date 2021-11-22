using System;

namespace ApiGameCatalog.Exceptions
{
    public class PublisherAlreadyExistsException : Exception
    {
        public PublisherAlreadyExistsException()
            : base("This publisher already exists")
        { }
    }
}
