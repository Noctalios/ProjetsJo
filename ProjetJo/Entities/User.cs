using ProjetsJo.Entities;

namespace ProjetsJo.Entities
{
    public class User
    {
        public Guid AccountKey { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public List<Ticket>? Tickets { get; set; }

        public User() { }
        public User(string firstName, string lastName, string email, Guid accountKey, bool isAdmin)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AccountKey = accountKey;
            IsAdmin = isAdmin;
        }
        public User(string firstName, string lastName, string email, Guid accountKey, bool isAdmin, List<Ticket>? tickets)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AccountKey = accountKey;
            IsAdmin= isAdmin;
            Tickets = tickets;
        }
    }
}