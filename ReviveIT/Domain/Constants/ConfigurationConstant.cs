namespace Domain.Constants
{
    public class ConfigurationConstant
    {
        public const string JwtSection = "Jwt";
        public const string ConnectionStringsSection = "ConnectionStrings";

        public const string Key = "Jwt:Key";
        public const string Issuer = "Jwt:Issuer";
        public const string Audience = "Jwt:Audience";
        public const string ExpiresInMinutes = "Jwt:ExpiresInMinutes";

        public const string RefreshTokenExpiresInDays = "Jwt:RefreshTokenExpiresInDays";
    }
}
