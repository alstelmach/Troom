namespace User.Infrastructure.Services.TokenGeneration
{
    public sealed class TokenSettings
    {
        public int ValidityTimeInMinutes { get; init; }
        public string SecurityKey { get; init; }
    }
}
