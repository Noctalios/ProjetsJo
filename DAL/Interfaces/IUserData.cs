using ProjetsJo.Entites;

namespace ProjetsJo.DAL.Interfaces
{
    public interface IUserData
    {
        List<Guid> GetUserGuids();
        User? GetUser(string firstName, string lastName, string password);
        void CreateUser(string firstName, string lastName, string email, Guid accountKey, string password);
    }
}
