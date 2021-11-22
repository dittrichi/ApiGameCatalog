using System;

namespace ApiGameCatalog.Exceptions
{
    public class PublisherNotFoundException : Exception
    {
        public PublisherNotFoundException()
            :base("Publisher not found")
        {

        }
    }
}
