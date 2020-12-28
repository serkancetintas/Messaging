using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Armut.Messaging.Infrastructure.Auth.Handlers
{
    public class JwtHandler : IJwtHandler
    {
        private static readonly IDictionary<string, IEnumerable<string>> EmptyClaims =
            new Dictionary<string, IEnumerable<string>>();

        private static readonly ISet<string> DefaultClaims = new HashSet<string>
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            ClaimTypes.Role,
        };

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;

        public JwtHandler(JwtOptions options)
        {
            _options = options;
        }

        public JsonWebToken CreateToken(string userId, string role = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(userId));
            }

            var now = DateTime.Now;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
            };
            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (!string.IsNullOrWhiteSpace(audience))
            {
                jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            }

            if (claims?.Any() is true)
            {
                var customClaims = new List<Claim>();
                foreach (var (claim, values) in claims)
                {
                    customClaims.AddRange(values.Select(value => new Claim(claim, value)));
                }

                jwtClaims.AddRange(customClaims);
            }

            var expires = _options.Expiry.HasValue
                ? now.AddMilliseconds(_options.Expiry.Value.TotalMilliseconds)
                : now.AddMinutes(_options.ExpiryMinutes);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: jwtClaims,
                audience: _options.Audience,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
                Id = userId,
                Role = role ?? string.Empty,
                Claims = claims ?? EmptyClaims
            };
        }

      
    }
}
