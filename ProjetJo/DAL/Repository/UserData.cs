using ProjetsJo.DAL.Interfaces;
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

        /// <summary>
        /// Provide the connection string from the Configuration.
        /// </summary>
        /// <returns>Return the value of DefaultConnection in appsettings.json.</returns>
        private string GetConnexionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// This method contains the password hashing algorithm.
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="firstName">FirstName of the User</param>
        /// <param name="lastName">LastName of the User</param>
        /// <returns>Return the HashPassword.</returns>
        private string HashPassword(string password, string firstName, string lastName)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes($"{password}{firstName}{lastName}");
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }

        #region Create

        /// <summary>
        /// Request the Database to execute the stored procedure CreateUser.
        /// The firs name, last name, email, accountKey and an hashed password are required.
        /// </summary>
        /// <param name="firstName">First name of the new User.</param>
        /// <param name="lastName">Last name of the new User.</param>
        /// <param name="email">Address email of the new User.</param>
        /// <param name="accountKey">AccountKey to register for the new User.</param>
        /// <param name="password">Hash Password to authenticate.</param>
        public void CreateUser(string firstName, string lastName, string email, Guid accountKey, string password)
        {
            string sql = "EXEC [CreateUser] @firstname, @lastName, @email, @accountKey, @password";
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@accountKey", accountKey);
                command.Parameters.AddWithValue("@password", HashPassword(password, firstName, lastName));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        #endregion

        #region Read
        /// <summary>
        /// Request the Database to excute the stored procedure GetUser 
        /// according first name, last name and a hashed password.
        /// </summary>
        /// <param name="firstName">First name of the User.</param>
        /// <param name="lastName">Last name of the User.</param>
        /// <param name="password">Password that must be hashed to connect.</param>
        /// <returns>If the authentication is successfull return User else return null.</returns>
        public User? GetUser(string firstName, string lastName, string password)
        {
            try
            {
                string sql = "EXEC [GetUser] @firstName, @lastName, @password ;";
                User? user = null;
                using (SqlConnection connection = new SqlConnection(GetConnexionString()))
                {
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@password", HashPassword(password, firstName, lastName));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User(
                            reader.GetString("FirstName"),
                            reader.GetString("LastName"),
                            reader.GetString("Mail"),
                            Guid.Parse(reader.GetString("AccountKey")),
                            reader.GetBoolean("IsAdmin")
                            );
                        }
                    }
                    connection.Close();
                    return user;
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
