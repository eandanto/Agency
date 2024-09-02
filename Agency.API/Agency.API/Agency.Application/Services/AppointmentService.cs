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
            model.Id = Guid.NewGuid();
            model.InsertedAt = DateTime.UtcNow;
            var appointment = _mapper.Map<Appointment>(model);
            var newAppointment = await _appointmentRepository.SetAppointment(appointment);
            return _mapper.Map<AppointmentDto>(newAppointment);
        }

        public async Task<AppointmentListDto> GetMyAppointments(Guid id, int pageNo, int pageSize)
        {
            var myAppointments = await _appointmentRepository.GetMyAppointments(id, pageNo, pageSize);
            var totalCount = await _appointmentRepository.GetMyAppointmentsCount(id);

            AppointmentListDto result = new AppointmentListDto();
            result.Appointments = _mapper.Map<List<AppointmentDto>>(myAppointments);
            result.TotalCounts = totalCount;

            return result;
        }

        public async Task<AppointmentListDto> GetAllAppointments(int pageNo, int pageSize, DateTime date)
        {
            var myAppointments = await _appointmentRepository.GetAllAppointments(pageNo, pageSize, date);
            var totalCount = await _appointmentRepository.GetAllAppointmentsCount(date);

            AppointmentListDto result = new AppointmentListDto();
            result.Appointments = _mapper.Map<List<AppointmentDto>>(myAppointments);
            result.TotalCounts = totalCount;

            return result;
        }
    }
}
