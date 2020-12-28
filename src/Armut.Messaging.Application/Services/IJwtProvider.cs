using Armut.Messaging.Application.DTO;
using System;
using System.Collections.Generic;

namespace Armut.Messaging.Application.Services
{
    public interface IJwtProvider
    {
        AuthDto Create(Guid userId, string role, string audience = null,
           IDictionary<string, IEnumerable<string>> claims = null);
    }
}
