using System;

namespace Armut.Messaging.Application.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string FromUserName { get; set; }
        public string MyUserName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
