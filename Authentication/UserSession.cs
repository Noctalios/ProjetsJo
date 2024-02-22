namespace ProjetsJo.Authentication
{
    public class UserSession
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        
        public string Hash { get; set; } = string.Empty;

        public Guid AccountKey { get; set; }
    }
}
