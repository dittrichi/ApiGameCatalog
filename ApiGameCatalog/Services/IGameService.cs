using ApiGameCatalog.InputModel;
using ApiGameCatalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Retrieve(int page, int amount);
        Task<GameViewModel> Retrieve(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Remove(Guid id);
    }
}
