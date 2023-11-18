using Microsoft.Data.SqlClient;
using RostrosFelices.Models;
using System.Data;

namespace RostrosFelices._Repositories
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("RostrosFelicesDB");
            Console.WriteLine($"Connection String: {this.connectionString}");
        }


        public IEnumerable<User> GetByValue(string value)
        {
            var userList = new List<User>();
            string userEmail = value;
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Users
                                    WHERE Email LIKE @email+ '%'
                                    ORDER By Id DESC";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userEmail;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }
    }
}
