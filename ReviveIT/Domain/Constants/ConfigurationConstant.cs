namespace Domain.Constants
{
    public static class ConfigurationConstant
    {
        public const string JwtSection = "Jwt";
        public const string ConnectionStringsSection = "ConnectionStrings";

        public const string Key = "Jwt:Key";
        public const string Issuer = "Jwt:Issuer";
        public const string Audience = "Jwt:Audience";
        public const string ExpiresInMinutes = "Jwt:ExpiresInMinutes";
    }
}
