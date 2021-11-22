using ApiGameCatalog.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public class GameSQLServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSQLServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task Insert(Game game)
        {
            var command = $"insert Games (Id, Name, PublisherId, Price) values('{game.Id}','{game.Name}','{game.PublisherId}','{game.Price.ToString().Replace(",",".")}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remove(Guid id)
        {
            var command = $"delete from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Game>> Retrieve(int page, int amount)
        {
            var games = new List<Game>();
            var command = $"select * from Games order by id offset {((page - 1) * amount)} rows fetch next {amount} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    PublisherId = (Guid)sqlDataReader["PublisherId"],
                    Price = (double)sqlDataReader["Price"]
                }); 
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<List<Game>> Retrieve(string name, Guid publisherId)
        {
            var games = new List<Game>();

            var command = $"select * from Games where Name ='{name}' and PublisherId ='{publisherId}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while(sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    PublisherId = (Guid)sqlDataReader["PublisherId"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task<Game> Retrieve(Guid id)
        {
            Game game = null;

            var command = $"select * from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while(sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    PublisherId = (Guid)sqlDataReader["PublisherId"],
                    Price = (double)sqlDataReader["Price"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task Update(Game game)
        {
            var command = $"update Games set Name = '{game.Name}', PublisherId = '{game.PublisherId}', Price = '{game.Price.ToString().Replace(",", ".")}' where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
