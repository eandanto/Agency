using Agency.Domain.Entities;
using Agency.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Infrastructure.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<bool> UpdateConfiguration(Configuration configuration);

        Task<List<Configuration>> GetAllConfigurations();
    }
}
