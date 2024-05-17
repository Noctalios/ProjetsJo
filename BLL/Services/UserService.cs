using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;

namespace ProjetsJo.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserData _userData;

        public UserService(IUserData userData)
        {
            _userData = userData;
        }

        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            Guid accountKey = Guid.Parse($"{firstName}+{DateTime.Now.ToString()}{lastName}");
            Console.WriteLine(accountKey);
            _userData.CreateUser(firstName, lastName, email, accountKey, password);
        }

        public User GetUser(string firstName, string lastName, string password)
        {
            return _userData.GetUser(firstName, lastName, password);
        }
    }
}
