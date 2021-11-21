using ApiGameCatalog.ViewModel;

namespace ApiGameCatalog.Configurations
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserViewModelOutput userViewModelOutput);
    }
}
