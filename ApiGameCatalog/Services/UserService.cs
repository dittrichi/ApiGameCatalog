using ApiGameCatalog.Entities;
using ApiGameCatalog.Repositories;
using ApiGameCatalog.ViewModel;
using ApiGameCatalog.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<UserViewModelOutput> RegisterUser(RegisterViewModelInput registroViewModelInput)
        {
            //TODO Add already existent user logic
            //var entityUser = await _userRepository.RetrieveUser(registroViewModelInput.Name, registroViewModelInput.Login);
            
            //TODO adjust all exceptions
            //if (entityUser.Count > 0)
            //    throw new Exception();

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

        public async Task<UserViewModelOutput> Retrieve(Guid idUser)
        {
            var user = await _userRepository.RetrieveUser(idUser);

            if (user == null)
                return null;

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
