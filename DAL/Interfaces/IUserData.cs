using ProjetsJo.Entites;

namespace ProjetsJo.DAL.Interfaces
{
    public interface IUserData
    {
        User GetUser(string firstName, string lastName, string password);

        void CreateUser(string userName, string email, string password);
    }
}
