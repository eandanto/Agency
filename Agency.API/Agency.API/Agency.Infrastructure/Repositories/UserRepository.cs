using Agency.Domain.Entities;
using Agency.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AgencyDbContext _context;

        public UserRepository(AgencyDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User model)
        {
            try
            {
                await _context.Users.AddAsync(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Login(User model)
        {
            try
            {
                var user = await _context.Users.Where(x => x.EmailAddress == model.EmailAddress && x.PasswordHash == model.PasswordHash).FirstOrDefaultAsync();
                if (user == null)
                    throw new Exception("Incorrect Email Address or Password");

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
