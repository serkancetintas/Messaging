using System;

namespace Armut.Messaging.Application
{
    public interface IUserContext
    {
        Guid Id { get; }
        bool IsAuthenticated { get; }
    }
}
