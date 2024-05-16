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
                command.Parameters.AddWithValue("@passpord", HashPassword(password, firstName, lastName));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        #endregion

        #region Read

        public User GetUser(string firstName, string lastName, string password)
        {
            User useraaa = new User( "Denis Lapa", "denis.lapa.pro@gmail.com", new Guid(),new Role(0, "Administrateur"));
            try
            {
                string sql = "EXEC [dbo].[GetUser] @firstName, @lastName, @password;";
                Dictionary<int, User> result= new();                 

                using (SqlConnection connection = new SqlConnection(GetConnexionString()))
                {
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@PassWord", HashPassword(password, firstName, lastName));
                
                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            if(result.TryGetValue(reader.GetInt32("Id"), out User currentUser))
                            {
                                currentUser.Tickets.Add
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
                                User newUser = new User();

                                result.Add(reader.GetInt32("Id"), newUser);
                            }
                        }
                    };
                    connection.Close();
                    return result.Values.First();
                };
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        #endregion
    }
}
