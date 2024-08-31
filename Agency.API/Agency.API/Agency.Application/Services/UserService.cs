using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Agency.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDto> Register(UserDto model)
        {
            if (model.EmailAddress == null)
                throw new Exception("Email Address cannot be empty");
            if (model.Password == null)
                throw new Exception("Password cannot be empty");
            if (model.UserOrCustomer != null)
            {
                model.UserOrCustomer = model.UserOrCustomer.Trim().ToUpper();
                if (model.UserOrCustomer != "CUSTOMER" && model.UserOrCustomer != "STAFF")
                    throw new Exception("User is not specified correctly");
            }
            else
                throw new Exception("User is not specified correctly");

            model.Id = Guid.NewGuid();
            model.Password = AppHelper.HashPassword(model.Password);

            var user = _mapper.Map<User>(model);
            var newUserCreated = await _userRepository.Register(user);
            return _mapper.Map<UserDto>(newUserCreated);
        }

        public async Task<string> Login(LoginDto model)
        {
            try
            {
                if (model.EmailAddress == null)
                    throw new Exception("Email Address cannot be empty");
                if (model.Password == null)
                    throw new Exception("Password cannot be empty");

                model.UserOrCustomer = model.UserOrCustomer.Trim().ToUpper();
                model.Password = AppHelper.HashPassword(model.Password);
                var user = _mapper.Map<User>(model);
                var userLoggedIn = await _userRepository.Login(user);

                if (userLoggedIn != null)
                {
                    // Generate JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                    if (key.Length < 32)
                    {
                        throw new ArgumentException("The JWT key must be at least 32 characters long.");
                    }
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, userLoggedIn.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.EmailAddress),
                        new Claim("UserOrCustomer", user.UserOrCustomer),
                        new Claim("CustomerId", userLoggedIn.Id.ToString()),
                    }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Audience"]
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);

                }
                throw new Exception("Invalid login credentials");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
