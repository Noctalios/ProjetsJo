using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;
using ProjetsJo.Entities;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace ProjetsJo.DAL.Repository
{
    public class UserData : IUserData
    {
        private IConfiguration _configuration;

        public UserData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnexionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        private string HashPassword(string password, string firstName, string lastName)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes($"{password}{firstName}{lastName}");
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }

        #region Create

        public void CreateUser(string firstName, string lastName, string email, Guid accountKey, string password)
        {
            string sql = "EXEC [CreateUser] @firstname, @lastName, @email, @accountKey, @password";
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@accountKey", accountKey);
                command.Parameters.AddWithValue("@password", HashPassword(password, firstName, lastName));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        #endregion

        #region Read

        public User? GetUser(string firstName, string lastName, string password)
        {
            try
            {
                string sql = "EXEC [dbo].[GetUser] @firstName, @lastName, @password;";
                Dictionary<Guid, User> result= new();                 

                using (SqlConnection connection = new SqlConnection(GetConnexionString()))
                {
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@password", HashPassword(password, firstName, lastName));
                
                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            if (result.TryGetValue(reader.GetGuid("AccountKey"), out User? currentUser))
                            {
                                currentUser.Tickets?.Add
                                (
                                    new Ticket
                                    (
                                        reader.GetInt32("Id"), 
                                        reader.GetString("QrCode"),
                                        reader.GetDateTime("Date")
                                    )
                                );
                            }
                            else
                            {
                                User newUser = new User
                                (
                                    reader.GetString("FirstName"),
                                    reader.GetString("LastName"),
                                    reader.GetString("Mail"),
                                    Guid.Parse(reader.GetString("AccountKey")),
                                    reader.GetBoolean("IsAdmin")
                                );
                                if(reader.IsDBNull(reader.GetInt32("Id")))
                                {
                                    newUser.Tickets = new();
                                    Ticket ticket = new Ticket
                                    (
                                        reader.GetInt32("Id"),
                                        reader.GetString("QrCode"),
                                        reader.GetDateTime("Date")
                                    );
                                }

                                result.Add(reader.GetGuid("AccountKey"), newUser);
                            }
                        }
                    };
                    connection.Close();
                    return result.Count > 0 ? result.Values.First() : null;
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Guid> GetUserGuids()
        {
            List<Guid> guids = new();

            string sql = "EXEC [GetUserGuids]";
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        guids.Add(Guid.Parse(reader.GetString("AccountKey")));
                    }
                };
                connection.Close();
            };
            return guids;
        }
        #endregion
    }
}
