using ApiGameCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public interface IPublisherRepository : IDisposable
    {
        Task<List<Publisher>> Retrieve();
        Task<Publisher> Retrieve(Guid id);
        Task<Publisher> Retrieve(string name);
        Task Insert(Publisher publisher);
        Task Update(Guid id, string name);
        Task Remove(Guid id);
    }
}
