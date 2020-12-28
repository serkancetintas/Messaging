using Armut.Messaging.Api.Controllers.Base;
using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.DTO;
using Armut.Messaging.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Armut.Messaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : BaseApiController
    {
        private readonly IQueryHandler<GetMessages, IEnumerable<MessageDto>> _queryHandler;
        private readonly ICommandHandler<SendMessage> _commandHandler;
        public MessageController(IQueryHandler<GetMessages, IEnumerable<MessageDto>> queryHandler,
                                 ICommandHandler<SendMessage> commandHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }


        [HttpGet("{FromUserName}")]
        public async Task<IEnumerable<MessageDto>> GetMessages([FromRoute]GetMessages getMessages)
        {
            return await _queryHandler.HandleAsync(getMessages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessage sendMessage)
        {
            await _commandHandler.HandleAsync(sendMessage);

            return Ok();
        }
    }
}
