﻿using System.Net;

namespace Armut.Messaging.Infrastructure.Exceptions
{
    public class ExceptionResponse
    {
        public object Response { get; }
        public HttpStatusCode StatusCode { get; }

        public ExceptionResponse(object response, HttpStatusCode statusCode)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
