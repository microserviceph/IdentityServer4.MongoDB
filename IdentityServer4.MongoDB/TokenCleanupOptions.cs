namespace IdentityServer4.MongoDB
{
    public class TokenCleanupOptions
    {
        public int Interval { get; set; } = 60;
    }
}