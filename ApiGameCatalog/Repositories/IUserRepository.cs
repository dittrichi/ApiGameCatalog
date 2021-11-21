using ApiGameCatalog.Entities;
using System;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task Create(User user);
        Task<User> RetrieveUser(string login);
    }
}
