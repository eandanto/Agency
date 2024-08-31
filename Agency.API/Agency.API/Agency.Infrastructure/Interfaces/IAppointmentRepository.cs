using Agency.Domain.Entities;
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
    }
}
