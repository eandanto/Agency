using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Agency.Infrastructure.Repositories;

namespace Agency.Application.Services
{
    public class OffDayService : IOffDayService
    {
        private readonly IOffDayRepository _offDayRepository;
        private readonly IMapper _mapper;

        public OffDayService(IOffDayRepository offDayRepository, IMapper mapper)
        {
            _offDayRepository = offDayRepository;
            _mapper = mapper;
        }

        public async Task<bool> SetOffDay(DateTime date)
        {
            return await _offDayRepository.SetOffDay(date);
        }

        public async Task<List<OffDayDto>> GetOffDays()
        {
            var result = await _offDayRepository.GetOffDays();
            return _mapper.Map<List<OffDayDto>>(result);
        }
        public async Task<bool> RemoveOffDay(DateTime date)
        {
            return await _offDayRepository.RemoveOffDay(date);
        }
    }
}
