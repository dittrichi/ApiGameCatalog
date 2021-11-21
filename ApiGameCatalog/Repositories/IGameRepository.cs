using ApiGameCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Retrieve(int page, int amount);
        Task<List<Game>> Retrieve(string name, string publisher);
        Task<Game> Retrieve(Guid id);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
    }
}
