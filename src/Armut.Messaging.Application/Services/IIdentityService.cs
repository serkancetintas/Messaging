using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.DTO;
using System.Threading.Tasks;

namespace Armut.Messaging.Application.Services
{
    public interface IIdentityService
    {
        Task<AuthDto> SignInAsync(SignIn command);
        Task SignUpAsync(SignUp command);
    }
}
