namespace Class.Converter
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneVerified { get; set; }
        public List<RoleDto>? Roles { get; set; }
        public bool Locked { get; set; }
        public bool Selected { get; set; }
    }

    public class RoleDto
    {
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
    }

    public static class UserConverter
    {
        public static UserData ConvertToUserData(this UserDto userDto)
        {
            return new UserData
            {
                Id = userDto.Id.ToString(),
                Username = userDto.Username,
                Email = userDto.Email,
                EmailVerified = userDto.EmailVerified,
                PhoneNumber = userDto.PhoneNumber,
                PhoneVerified = userDto.PhoneVerified,
                Roles = userDto.Roles?.ConvertAll(roleDto => new Role
                {
                    Id = roleDto.RoleId,
                    RoleName = roleDto.RoleName,
                    UserCount = null // Set as necessary
                }),
                Locked = userDto.Locked,
                Selected = userDto.Selected
            };
        }
    }

}
