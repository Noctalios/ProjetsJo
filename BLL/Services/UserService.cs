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
        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            _userData.CreateUser(firstName, lastName, email, Guid.NewGuid(), password);
        }

        public User? GetUser(string firstName, string lastName, string password)
        {
            return _userData.GetUser(firstName, lastName, password);
        }
        #endregion

    }
}
