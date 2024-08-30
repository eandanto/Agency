using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;

namespace Agency.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
                if (model.UserOrCustomer != "CUSTOMER" || model.UserOrCustomer != "STAFF")
                    throw new Exception("User is not specified correctly");
            }
            else
                throw new Exception("User is not specified correctly");

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                PasswordHash = AppHelper.HashPassword(model.Password),
                UserOrCustomer = model.UserOrCustomer.Trim().ToUpper()
            };
            var user = _mapper.Map<User>(newUser);
            var newUserCreated = await _userRepository.Register(user);
            return _mapper.Map<UserDto>(newUserCreated);
        }

        public async Task<bool> Login(LoginDto model)
        {
            try
            {
                if (model.EmailAddress == null)
                    throw new Exception("Email Address cannot be empty");
                if (model.Password == null)
                    throw new Exception("Password cannot be empty");

                model.Password = AppHelper.HashPassword(model.Password);
                var user = _mapper.Map<User>(model);
                return await _userRepository.Login(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
