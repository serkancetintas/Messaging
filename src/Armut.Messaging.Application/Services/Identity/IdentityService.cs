using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.DTO;
using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Exceptions;
using Armut.Messaging.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Armut.Messaging.Application.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private static readonly Regex EmailRegex = new Regex(
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex UserNameRegex = new Regex(
            @"^[a-zA-Z][a-zA-Z0-9]{3,15}$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant
            );
            
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(IUserRepository userRepository,
                               IPasswordService passwordService,
                               IJwtProvider jwtProvider,
                               ILogger<IdentityService> logger)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtProvider = jwtProvider;
            _logger = logger;
        }

        public async Task<AuthDto> SignInAsync(SignIn command)
        {
            var user = await _userRepository.GetByUserNameAsync(command.UserName);
            if (user is null)
            {
                _logger.LogError($"User with user name: {command.UserName} was not found.");
                throw new InvalidCredentialsException(command.UserName);
            }

            if (!_passwordService.IsValid(user.Password, command.Password))
            {
                _logger.LogError($"Invalid password for user with id: {user.Id.Value}");
                throw new InvalidCredentialsException(command.UserName);
            }

            var auth = _jwtProvider.Create(user.Id, "user");
            _logger.LogInformation($"User with id: {user.Id} has been authenticated.");

            return auth;
        }


        public async Task SignUpAsync(SignUp command)
        {
            if (!EmailRegex.IsMatch(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidEmailException(command.Email);
            }

            if (!UserNameRegex.IsMatch(command.UserName))
            {
                _logger.LogError($"Invalid user name: {command.UserName}");
                throw new InvalidUserNameException(command.UserName);
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidPassword = hasNumber.IsMatch(command.Password) &&
                                  hasUpperChar.IsMatch(command.Password) &&
                                  hasMinimum8Chars.IsMatch(command.Password);

            if (!isValidPassword)
            {
                _logger.LogError("Invalid password.");
                throw new InvalidPasswordException();
            }

            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user is { })
            {
                _logger.LogError($"Email already in use: {command.Email}");
                throw new EmailInUseException(command.Email);
            }

            user = await _userRepository.GetByUserNameAsync(command.UserName);
            if (user is { })
            {
                _logger.LogError($"User name already in use: {command.UserName}");
                throw new UserNameInUseException(command.UserName);
            }

            var password = _passwordService.Hash(command.Password);
            user = new User(Guid.NewGuid(), command.Email, command.UserName, password, DateTime.Now);
            await _userRepository.AddAsync(user);

            _logger.LogInformation($"Created an account for the user with id: {user.Id}.");
        }

    }
}
