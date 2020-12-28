using System;

namespace Armut.Messaging.Infrastructure.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
