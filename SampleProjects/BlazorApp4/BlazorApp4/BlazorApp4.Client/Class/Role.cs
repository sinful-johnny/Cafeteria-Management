namespace Class
{
    public class Role
    {
        public required int Id { get; set; }
        public required string RoleName { get; set; }
        public int? UserCount { get; set; }
    }
}
