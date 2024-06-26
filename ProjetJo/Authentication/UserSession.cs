﻿namespace ProjetsJo.Authentication
{
    public class UserSession
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Guid AccountKey { get; set; }
    }
}
