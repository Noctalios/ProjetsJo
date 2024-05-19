using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;
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
            List<Guid> existingGuids = _userData.GetUserGuids();
            Guid accountKey = new();
            VerifyAccountKey(ref accountKey, existingGuids);
            _userData.CreateUser(firstName, lastName, email, accountKey, password);
        }

        public User? GetUser(string firstName, string lastName, string password)
        {
            return _userData.GetUser(firstName, lastName, password);
        }
        #endregion

        #region Technical
        private void VerifyAccountKey(ref Guid accountKey, List<Guid> existingGuids)
        {
            accountKey = Guid.NewGuid();
            if (existingGuids.Contains(accountKey))
                VerifyAccountKey(ref accountKey, existingGuids);
            return;    
        }
        #endregion
    }
}
