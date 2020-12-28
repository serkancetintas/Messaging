using Armut.Messaging.Application.DTO;
using Armut.Messaging.Application.Services;
using System;
using System.Collections.Generic;

namespace Armut.Messaging.Infrastructure.Auth
{
    public class JwtProvider: IJwtProvider
    {
        private readonly IJwtHandler _jwtHandler;

        public JwtProvider(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        public AuthDto Create(Guid userId, string role, string audience = null,
           IDictionary<string, IEnumerable<string>> claims = null)
        {
            var jwt = _jwtHandler.CreateToken(userId.ToString(), role, audience, claims);

            return new AuthDto
            {
                AccessToken = jwt.AccessToken,
                Expires = jwt.Expires
            };
        }
    }
}
