using ApiGameCatalog.Entities;
using ApiGameCatalog.Exceptions;
using ApiGameCatalog.Repositories;
using ApiGameCatalog.ViewModel;
using ApiGameCatalog.ViewModel.Users;
using System;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Dispose()
        {
            _userRepository.Dispose();
        }

        public async Task<UserViewModelOutput> RegisterUser(RegisterInputModel registroViewModelInput)
        {            
            var user = await _userRepository.RetrieveUser(registroViewModelInput.Login);
                        
            if (user != null)
                throw new UserAlreadyExistsException();

            var userRegister = new User
            {
                Id = Guid.NewGuid(),
                Login = registroViewModelInput.Login,
                Name = registroViewModelInput.Name,
                Email = registroViewModelInput.Email,
                Password = registroViewModelInput.Password
            };

            await _userRepository.Create(userRegister);

            return new UserViewModelOutput
            {
                Id = userRegister.Id,
                Login = userRegister.Login,
                Name = userRegister.Name,                
                Email = userRegister.Email
            };
        }

        public async Task<UserViewModelOutput> Login(LoginInputModel loginViewModelInput)
        {
            var user = await _userRepository.RetrieveUser(loginViewModelInput.Login);

            if (user == null)
                throw new LoginErrorException();

            if (user.Password != loginViewModelInput.Password)
                throw new LoginErrorException();

            return new UserViewModelOutput
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
