using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Infrastructure.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> SetAppointment(Appointment appointment);
        Task<List<Appointment>> GetMyAppointments(Guid id, int pageNo, int pageSize);
        Task<int> GetMyAppointmentsCount(Guid id);
        Task<List<Appointment>> GetAllAppointments(int pageNo, int pageSize, DateTime date);
        Task<int> GetAllAppointmentsCount(DateTime date);
    }
}
