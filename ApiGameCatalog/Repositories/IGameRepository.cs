using ApiGameCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Retrieve(int page, int amount);
        Task<List<Game>> RetrieveByPublisher(Guid publisherId);
        Task<Game> Retrieve(string name, Guid publisherId);
        Task<Game> Retrieve(Guid id);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
    }
}
