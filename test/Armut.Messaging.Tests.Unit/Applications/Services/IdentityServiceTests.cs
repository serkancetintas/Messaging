using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.Services;
using Armut.Messaging.Application.Services.Identity;
using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Exceptions;
using Armut.Messaging.Core.Repositories;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Messaging.Tests.Unit.Applications.Services
{
    public class IdentityServiceTests
    {
        [Theory, AutoMoqData]
        public async Task given_invalid_email_sign_up_should_throw_an_exception
            (IdentityService identityService)
        {
            string email = "abb.com";
            string userName = "testuser";
            string password = "abRcdf14";
            var signUp = new SignUp(email, userName, password);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignUpAsync(signUp));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidEmailException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_username_sign_up_should_throw_an_exception
           (IdentityService identityService)
        {
            string email = "test@gmail.com";
            string userName = "aa";
            string password = "abRcdf14";
            var signUp = new SignUp(email, userName, password);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignUpAsync(signUp));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidUserNameException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_password_sign_up_should_throw_an_exception
           (IdentityService identityService)
        {
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = "abrcdf14";
            var signUp = new SignUp(email, userName, password);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignUpAsync(signUp));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidPasswordException>();
        }

        [Theory, AutoMoqData]
        public async Task given_mail_exists_before_sign_up_should_throw_an_exception
          ([Frozen] Mock<IUserRepository> userRepository,
            User user,
            IdentityService identityService)
        {
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = "abRcdf14";
            var signUp = new SignUp(email, userName, password);
            
            userRepository.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(user);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignUpAsync(signUp));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<EmailInUseException>();
        }

        [Theory, AutoMoqData]
        public async Task given_username_exists_before_sign_up_should_throw_an_exception
         ([Frozen] Mock<IUserRepository> userRepository,
           User user,
           IdentityService identityService)
        {
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = "abRcdf14";
            var signUp = new SignUp(email, userName, password);

            User nullUser = null;
            userRepository.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(nullUser);
            userRepository.Setup(r => r.GetByUserNameAsync(userName)).ReturnsAsync(user);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignUpAsync(signUp));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<UserNameInUseException>();
        }

        [Theory, AutoMoqData]
        public async Task given_valid_parameters_sign_up_should_success
         ([Frozen] Mock<IUserRepository> userRepository,
          [Frozen] Mock<IPasswordService> passwordService,
           IdentityService identityService)
        {
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = "abRcdf14";
            var signUp = new SignUp(email, userName, password);

            User nullUser = null;
            userRepository.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(nullUser);
            userRepository.Setup(r => r.GetByUserNameAsync(userName)).ReturnsAsync(nullUser);
            passwordService.Setup(r => r.Hash(password)).Returns("ee181fec973cf27270be737d39e996d504730cd680f8bcc75c15dde10ae3dde9");

            var exception = await Record.ExceptionAsync(async () => await identityService.SignUpAsync(signUp));

            exception.Should().BeNull();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_username_sign_in_should_throw_an_exception
           ([Frozen] Mock<IUserRepository> userRepository,
            IdentityService identityService)
        {
            string userName = "testuser";
            string password = "abRcdf14";
            var signIn = new SignIn(userName, password);

            User nullUser = null;
            userRepository.Setup(r => r.GetByUserNameAsync(userName)).ReturnsAsync(nullUser);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignInAsync(signIn));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidCredentialsException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_password_sign_in_should_throw_an_exception
          ([Frozen] Mock<IUserRepository> userRepository,
           [Frozen] Mock<IPasswordService> passwordService,
           User user,
           IdentityService identityService)
        {
            string userName = "testuser";
            string password = "abRcdf14";
            var signIn = new SignIn(userName, password);

            userRepository.Setup(r => r.GetByUserNameAsync(userName)).ReturnsAsync(user);
            passwordService.Setup(r => r.IsValid(user.Password, password)).Returns(false);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignInAsync(signIn));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidCredentialsException>();
        }

        [Theory, AutoMoqData]
        public async Task given_valid_parameters_sign_in_should_success
          ([Frozen] Mock<IUserRepository> userRepository,
           [Frozen] Mock<IPasswordService> passwordService,
           User user,
           IdentityService identityService)
        {
            string userName = "testuser";
            string password = "abRcdf14";
            var signIn = new SignIn(userName, password);

            userRepository.Setup(r => r.GetByUserNameAsync(userName)).ReturnsAsync(user);
            passwordService.Setup(r => r.IsValid(user.Password, password)).Returns(true);

            var exception = await Record.ExceptionAsync(async () => await identityService.SignInAsync(signIn));

            exception.Should().BeNull();
        }

    }
}
