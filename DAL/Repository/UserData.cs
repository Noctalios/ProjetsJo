using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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

        private string HashPassword(string password, string email)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes($"{password}{email}");
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }

        #region Create

        public void CreateUser(string userName, string email, string password)
        {
            string sql = "";
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);


                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PassWord", HashPassword(password, email));
                
            }
        }
        
        #endregion

        #region Read
        
        public User GetUser(string email, string password)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}
