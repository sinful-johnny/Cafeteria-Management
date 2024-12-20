using Class;

namespace Class
{
    public class UserData
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneVerified { get; set; }
        public List<Role>? Roles { get; set; }
        public bool Locked { get; set; }
        public bool Selected { get; set; }
    }
}
