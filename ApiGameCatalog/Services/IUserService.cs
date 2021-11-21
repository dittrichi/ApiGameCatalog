using ApiGameCatalog.ViewModel;
using ApiGameCatalog.ViewModel.Users;
using System;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public interface IUserService : IDisposable
    {
        Task<UserViewModelOutput> Login(LoginInputModel loginViewModelInput);

        Task<UserViewModelOutput> RegisterUser(RegisterInputModel registroViewModelInput);        
    }
}
