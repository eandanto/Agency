using Agency.Domain.Entities;
using Agency.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly AgencyDbContext _context;

        public ConfigurationRepository(AgencyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateConfiguration(Configuration model)
        {
            try
            {
                var configuration = await _context.Configurations.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (configuration == null)
                    throw new Exception("Unable to find this specific configuration");

                configuration.Value = model.Value;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Configuration>> GetAllConfigurations()
        {
            return await _context.Configurations.ToListAsync();
        }
    }
}
