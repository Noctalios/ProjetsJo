using ProjetsJo.Entities;

namespace ProjetsJo.Entites
{
    public class User
    {
        public Guid AccountKey { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public List<string>? Tickets { get; set; }

        public User() { }
        public User(string userName, string email, string password, Guid accountKey, Role role)
        {
            UserName = userName;
            Email = email;
            Password = password;
            AccountKey = accountKey;
            Role = role;
        }
        public User( string userName, string email, string password, Guid accountKey, Role role, List<string>? tickets)
        {
            UserName = userName;
            Email = email;
            Password = password;
            AccountKey = accountKey;
            Role = role;
            Tickets = tickets;
        }
    }
}