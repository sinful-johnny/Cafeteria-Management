namespace Class.Auth
{
    public class CustomClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public CustomClaim(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
