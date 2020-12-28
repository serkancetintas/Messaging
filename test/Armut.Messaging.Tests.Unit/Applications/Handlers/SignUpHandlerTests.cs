using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.Commands.Handlers;
using Armut.Messaging.Application.Services;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Messaging.Tests.Unit.Applications.Handlers
{
    public class SignUpHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task given_valid_signup_signuphandler_should_success([Frozen] Mock<IIdentityService> identityService,
                                                                          SignUpHandler handler)
        {
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = "abRcdf14";

            var signUp = new SignUp(email, userName, password);

            identityService.Setup(r => r.SignUpAsync(signUp)).Verifiable();
            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(signUp));

            exception.Should().BeNull();
        }
    }
}
