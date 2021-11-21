using ApiGameCatalog.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ApiGameCatalog.Repositories
{
    public class UserSQLServerRepository : IUserRepository
    {
        private readonly SqlConnection sqlConnection;

        public UserSQLServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Create(User user)
        {
            var command = $"insert Users (Id, Login, Name, Email, Password) values('{user.Id}','{user.Login}','{user.Name}','{user.Email}','{user.Password}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task<User> RetrieveUser(Guid id)
        {
            User user = null;

            var command = $"select * from Users where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                user = new User
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Login = (string)sqlDataReader["Login"],
                    Name = (string)sqlDataReader["Name"],
                    Email = (string)sqlDataReader["Email"],
                    Password = (string)sqlDataReader["Password"]
                };
            }

            await sqlConnection.CloseAsync();

            return user;
        }
    }
}
