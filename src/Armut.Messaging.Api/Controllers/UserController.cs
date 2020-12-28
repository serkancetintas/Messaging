using Armut.Messaging.Api.Controllers.Base;
using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Armut.Messaging.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController:BaseApiController
    {
        private readonly ICommandHandler<SignUp> _commandHandler;
        private readonly IIdentityService _identityService;
        public UserController(ICommandHandler<SignUp> commandHandler,
                             IIdentityService identityService)
        {
            _commandHandler = commandHandler;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUp signUp)
        {
            await _commandHandler.HandleAsync(signUp);

            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignIn signIn)
        {
            var token = await _identityService.SignInAsync(signIn);

            return Ok(token);
        }

    }
}
