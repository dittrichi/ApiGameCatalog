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
                PublisherID = game.PublisherId,
                Publisher = game.Publisher.Name,
                Price = game.Price
            }).ToList();
        }
        
        public async Task<List<GameViewModel>> RetrieveByPublisher(Guid publisherId)
        {
            var games = await _gameRepository.RetrieveByPublisher(publisherId);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                PublisherID = game.PublisherId,
                Publisher = game.Publisher.Name,
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
                PublisherID = game.PublisherId,
                Publisher = game.Publisher.Name,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
           var entityGame = await _gameRepository.Retrieve(game.Name, game.PublisherId);

            if (entityGame != null)
                throw new GameAlreadyExistsException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                PublisherId = game.PublisherId,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);
            gameInsert = await _gameRepository.Retrieve(gameInsert.Id);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                PublisherID = game.PublisherId,
                Publisher = gameInsert.Publisher.Name,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.Retrieve(id);

            if (entityGame == null)
                throw new GameNotFoundException();

            entityGame.Name = game.Name;
            entityGame.PublisherId = game.PublisherId;
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
