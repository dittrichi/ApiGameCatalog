using ApiGameCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{/*
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            //{Guid.Parse("7194f7a6-c431-4463-980e-ac3d6f818ae0"), new Game{ Id = Guid.Parse("7194f7a6-c431-4463-980e-ac3d6f818ae0"), Name = "World of Warcraft", Publisher = "Blizzard", Price = 150 } },
            //{Guid.Parse("4b08ee79-62ba-45b5-9cb3-014bbed7a03c"), new Game{ Id = Guid.Parse("4b08ee79-62ba-45b5-9cb3-014bbed7a03c"), Name = "Heroes of the Storm", Publisher = "Blizzard", Price = 200 } },
            //{Guid.Parse("ddf434f8-162a-46e2-baa3-2d3c307c1fb6"), new Game{ Id = Guid.Parse("ddf434f8-162a-46e2-baa3-2d3c307c1fb6"), Name = "Overwatch", Publisher = "Blizzzard", Price = 180 } },
            //{Guid.Parse("0f41ca00-c5d8-4e20-b995-bef87a45b71d"), new Game{ Id = Guid.Parse("0f41ca00-c5d8-4e20-b995-bef87a45b71d"), Name = "Grand Theft Auto V", Publisher = "Rockstar", Price = 190 } },
            //{Guid.Parse("f0b2f9e9-ce56-49a1-8993-14eba8fd68d1"), new Game{ Id = Guid.Parse("f0b2f9e9-ce56-49a1-8993-14eba8fd68d1"), Name = "New World", Publisher = "Amazon", Price = 70 } },
            //{Guid.Parse("ff7eebdc-45b2-43b3-b8f7-9a8a77311978"), new Game{ Id = Guid.Parse("ff7eebdc-45b2-43b3-b8f7-9a8a77311978"), Name = "Star Wars Battlefront II", Publisher = "EA", Price = 90 } }
        };

        public Task<List<Game>> Retrieve(int page, int amount)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * amount).Take(amount).ToList());
        }

        public Task<Game> Retrieve(Guid id)
        {
            if (!games.ContainsKey(id))
                return null;

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Retrieve(string name, Guid publisherID)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.PublisherId.Equals(publisherID)).ToList());
        }

        //This method have exactly the same result as the method above, just wrote for didatic reasons, showing the difference of using lambda expressions
        public Task<List<Game>> RetrieveWihtoutLambda(string name, Guid publisherId)
        {
            var result = new List<Game>();

            foreach(var game in games.Values)
            {
                if (game.Name.Equals(name) && game.PublisherId.Equals(publisherId))
                    result.Add(game);
            }
            return Task.FromResult(result);
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Close database connection 
        }
    }*/
}
