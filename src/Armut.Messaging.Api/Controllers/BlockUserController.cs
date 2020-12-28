using Armut.Messaging.Api.Controllers.Base;
using Armut.Messaging.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Armut.Messaging.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BlockUserController : BaseApiController
    {
        private readonly ICommandHandler<Block> _commandHandler;
        public BlockUserController(ICommandHandler<Block> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser([FromBody] Block block)
        {
            await _commandHandler.HandleAsync(block);

            return Ok();
        }
    }
}
