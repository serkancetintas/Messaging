using Armut.Messaging.Application.DTO;
using System;
using System.Collections.Generic;

namespace Armut.Messaging.Application.Queries
{
    public class GetMessages: IQuery<IEnumerable<MessageDto>>
    {
        public string FromUserName { get; set; }
    }
}
