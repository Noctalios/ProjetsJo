using ProjetsJo.Entites;

namespace ProjetsJo.BLL.Interfaces
{
    public interface IUserService
    {
        User GetUser(string email, string password);
        
        void CreateUser(string userName, string email, string password);
    }
}
