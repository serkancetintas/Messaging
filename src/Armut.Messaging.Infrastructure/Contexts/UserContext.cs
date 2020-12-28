using Armut.Messaging.Application;
using Microsoft.AspNetCore.Http;
using System;

namespace Armut.Messaging.Infrastructure.Contexts
{
    public class UserContext: IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Id => Guid.TryParse(_httpContextAccessor.HttpContext.User.Identity.Name, out var userId) ? userId : Guid.Empty;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
