using ApiGameCatalog.ViewModel;
using ApiGameCatalog.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public interface IUserService : IDisposable
    {
        Task<UserViewModelOutput> Retrieve(Guid idUser);

        Task<UserViewModelOutput> RegisterUser(RegisterViewModelInput registroViewModelInput);
    }
}
