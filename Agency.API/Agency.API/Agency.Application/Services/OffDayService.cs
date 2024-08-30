using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;

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

        public async Task<bool> SetOffDay(OffDayDto model)
        {
            if (model.Day == null)
                throw new Exception("Date is not specified correctly");

            var offDay = _mapper.Map<OffDay>(model);
            return await _offDayRepository.SetOffDay(offDay);
        }
    }
}
