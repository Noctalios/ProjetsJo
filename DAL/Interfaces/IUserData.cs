using ProjetsJo.Entities;

namespace ProjetsJo.DAL.Interfaces
{
    public interface IUserData
    {
        User? GetUser(string firstName, string lastName, string password);
        void CreateUser(string firstName, string lastName, string email, Guid accountKey, string password);
    }
}
