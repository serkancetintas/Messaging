using System;

namespace Armut.Messaging.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; }
        public TimeSpan? Expiry { get; set; }
        public string Secret { get; set; }
    }
}
