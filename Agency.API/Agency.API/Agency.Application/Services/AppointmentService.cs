using Agency.Domain.Entities;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Agency.Infrastructure.Interfaces;
using AutoMapper;
using System.Reflection;
using Agency.Infrastructure.Repositories;

namespace Agency.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> SetAppointment(AppointmentDto model)
        {
            if (model.Id == null)
                throw new Exception("Id cannot be null");

            model.Id = Guid.NewGuid();
            var appointment = _mapper.Map<Appointment>(model);
            var newAppointment = await _appointmentRepository.SetAppointment(appointment);
            return _mapper.Map<AppointmentDto>(newAppointment);
        }
    }
}
