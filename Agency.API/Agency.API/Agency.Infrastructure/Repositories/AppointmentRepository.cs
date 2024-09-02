using Agency.Domain.Entities;
using Agency.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AgencyDbContext _context;

        public AppointmentRepository(AgencyDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> SetAppointment(Appointment model)
        {
            try
            {
                var maxAppointmentPerDayQuery = await _context.Configurations.Where(x => x.PropertyName == "MAX_APPOINTMENTS").Select(x => x.Value).FirstOrDefaultAsync();
                if (maxAppointmentPerDayQuery == null)
                    throw new Exception("Maximum appointments per day is not set");
                int maxAppointmentPerDay = Convert.ToInt32(maxAppointmentPerDayQuery);

                var isOffDay = await _context.OffDays.Where(x => x.Day == model.AppointmentDate).FirstOrDefaultAsync();
                if (isOffDay != null)
                    throw new Exception("Selected Date is an Off Day");

                var appointmentCount = await _context.Appointments.Where(x => x.AppointmentDate == model.AppointmentDate).CountAsync();
                if (appointmentCount >= maxAppointmentPerDay)
                {
                    var isSearching = true;
                    DateTime nextAvailableDate = model.AppointmentDate;
                    while (isSearching)
                    {
                        nextAvailableDate = nextAvailableDate.AddDays(1);
                        var appointmentCountDay = await _context.Appointments.Where(x => x.AppointmentDate == nextAvailableDate).CountAsync();
                        isOffDay = await _context.OffDays.Where(x => x.Day == nextAvailableDate).FirstOrDefaultAsync();
                        if (appointmentCountDay <= maxAppointmentPerDay && isOffDay == null)
                            isSearching = false;
                    }
                    model.AppointmentDate = nextAvailableDate;
                }

                model.Token = GenerateAppointmentToken(model.CustomerId, model.AppointmentDate);

                await _context.Appointments.AddAsync(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Appointment>> GetMyAppointments(Guid id, int pageNo, int pageSize)
        {
            var myAppointments = await _context.Appointments.Where(x => x.CustomerId == id).OrderByDescending(x => x.AppointmentDate).Skip(pageNo * pageSize).Take(pageSize).ToListAsync();

            foreach (var item in myAppointments)
                item.InsertedAt = item.InsertedAt.ToLocalTime();

            return myAppointments;
        }

        public async Task<int> GetMyAppointmentsCount(Guid id)
        {
            return await _context.Appointments.Where(x => x.CustomerId == id).CountAsync();
        }

        public async Task<List<Appointment>> GetAllAppointments(int pageNo, int pageSize, DateTime date)
        {
            return await _context.Appointments.Include(x => x.Customer).Where(x => x.AppointmentDate == date).OrderByDescending(x => x.AppointmentDate).Skip(pageNo * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetAllAppointmentsCount(DateTime date)
        {
            return await _context.Appointments.Where(x => x.AppointmentDate == date).CountAsync();
        }

        private string GenerateAppointmentToken(Guid customerId, DateTime appointmentDate)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Combine the key components into a single string
                string rawToken = $"{customerId}-{appointmentDate:yyyyMMddHHmmss}-{Guid.NewGuid()}";

                // Compute the hash of the combined string
                byte[] hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawToken));

                // Encode the hash as a Base64 string and take the first 16 characters to shorten the token
                string token = Convert.ToBase64String(hash).Replace("=", "").Replace("+", "").Replace("/", "").Substring(0, 16);

                return token;
            }
        }

    }
}
