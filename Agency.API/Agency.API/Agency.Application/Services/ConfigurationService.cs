using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;

namespace Agency.Application.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMapper _mapper;

        public ConfigurationService(IConfigurationRepository configurationRepository, IMapper mapper)
        {
            _configurationRepository = configurationRepository;
            _mapper = mapper;
        }

        public async Task<bool> SetConfiguration(ConfigurationDto model)
        {
            if (model.Id == null)
                throw new Exception("Id cannot be null");

            var configuration = _mapper.Map<Configuration>(model);
            return await _configurationRepository.SetConfiguration(configuration);
        }
    }
}
