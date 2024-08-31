using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Configuration;

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

        public async Task<bool> UpdateConfiguration(ConfigurationDto model)
        {
            if (model.Id == null)
                throw new Exception("Id cannot be null");

            var configuration = _mapper.Map<Configuration>(model);
            return await _configurationRepository.UpdateConfiguration(configuration);
        }

        public async Task<List<ConfigurationDto>> GetAllConfigurations()
        {
            var result = await _configurationRepository.GetAllConfigurations();
            return _mapper.Map<List<ConfigurationDto>>(result);
        }
    }
}
