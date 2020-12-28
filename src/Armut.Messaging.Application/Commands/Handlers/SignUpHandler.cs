using Armut.Messaging.Application.Services;
using System.Threading.Tasks;

namespace Armut.Messaging.Application.Commands.Handlers
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IIdentityService _identityService;

        public SignUpHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public Task HandleAsync(SignUp command) =>  _identityService.SignUpAsync(command);
    }
}
