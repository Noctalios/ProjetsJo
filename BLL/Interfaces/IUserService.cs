using ProjetsJo.Entites;

namespace ProjetsJo.BLL.Interfaces
{
    public interface IUserService
    {
        User GetUser(string firstName, string lastName, string password);
        
        void CreateUser(string userName, string email, string password);
    }
}
