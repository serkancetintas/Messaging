using System.Collections.Generic;

namespace Armut.Messaging.Infrastructure.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null, string audience = null,
           IDictionary<string, IEnumerable<string>> claims = null);
    }
}
