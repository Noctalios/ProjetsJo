using ProjetsJo.Entities;

namespace ProjetsJo.BLL.Interfaces
{
    public interface IUserService
    {
        User? GetUser(string firstName, string lastName, string password);
        void CreateUser(string firstName, string lastName, string email, string password);
    }
}
