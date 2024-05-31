using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;
using System.Linq;

namespace ProjetsJo.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserData _userData;

        public UserService(IUserData userData)
        {
            _userData = userData;
        }

        #region Interface Implementation
        /// <summary>
        /// This methods requires the DAL to create a new User providing first name, last  name, email and password.
        /// </summary>
        /// <param name="firstName">First name of the new User.</param>
        /// <param name="lastName">Last name of the new Use</param>
        /// <param name="email">Email address of the new User.</param>
        /// <param name="password">Password of the new User to authenticate on connexion.</param>
        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            _userData.CreateUser(firstName, lastName, email, Guid.NewGuid(), password);
        }

        /// <summary>
        /// This method requires the DAL to get the User according firstName, lastName and password.
        /// </summary>
        /// <param name="firstName">First name of the User.</param>
        /// <param name="lastName">Last name of the User.</param>
        /// <param name="password">Password for to authenticate the user.</param>
        /// <returns></returns>
        public User? GetUser(string firstName, string lastName, string password)
        {
            return _userData.GetUser(firstName, lastName, password);
        }
        #endregion

    }
}
