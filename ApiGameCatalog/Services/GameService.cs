using ApiGameCatalog.Entities;
using ApiGameCatalog.Exceptions;
using ApiGameCatalog.InputModel;
using ApiGameCatalog.Repositories;
using ApiGameCatalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Retrieve(int page, int amount)
        {
            var games = await _gameRepository.Retrieve(page, amount);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Retrieve(Guid id)
        {
            var game = await _gameRepository.Retrieve(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entityGame = await _gameRepository.Retrieve(game.Name, game.Publisher);

            if (entityGame.Count > 0)
                throw new GameAlreadyExistentException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.Retrieve(id);

            if (entityGame == null)
                throw new GameNotFoundException();

            entityGame.Name = game.Name;
            entityGame.Publisher = game.Publisher;
            entityGame.Price = game.Price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Update(Guid id, double price)
        {
            var entityGame = await _gameRepository.Retrieve(id);

            if (entityGame == null)
                throw new GameNotFoundException();
                        
            entityGame.Price = price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.Retrieve(id);

            if (game == null)
                throw new GameNotFoundException();

            await _gameRepository.Remove(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
