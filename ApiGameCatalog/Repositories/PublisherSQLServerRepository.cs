using ApiGameCatalog.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public class PublisherSQLServerRepository : IPublisherRepository
    {
        private readonly SqlConnection sqlConnection;

        public PublisherSQLServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task Insert(Publisher publisher)
        {
            var command = $"insert Publishers (Id, Name) values('{publisher.Id}','{publisher.Name}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remove(Guid id)
        {
            var command = $"delete from Publishers where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Publisher>> Retrieve()
        {
            var publishers = new List<Publisher>();
            var command = $"select * from Publishers order by id";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                publishers.Add(new Publisher
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"]
                });
            }

            await sqlConnection.CloseAsync();

            return publishers;
        }

        public async Task<Publisher> Retrieve(Guid id)
        {
            Publisher publisher = null;

            var command = $"select * from Publishers where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                publisher = new Publisher
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"]
                };
            }

            await sqlConnection.CloseAsync();

            return publisher;
        }

        public async Task<Publisher> Retrieve(string name)
        {
            Publisher publisher = null;

            var command = $"select * from Publishers where Name = '{name}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                publisher = new Publisher
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"]
                };
            }

            await sqlConnection.CloseAsync();

            return publisher;
        }

        public async Task Update(Guid id, string name)
        {
            var command = $"update Publishers set Name = '{name}' where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
