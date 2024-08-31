using Agency.Domain.Entities;
using Agency.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Repositories
{
    public class OffDayRepository : IOffDayRepository
    {
        private readonly AgencyDbContext _context;

        public OffDayRepository(AgencyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetOffDay(DateTime date)
        {
            try
            {
                var offDay = await _context.OffDays.Where(x => x.Day == date).FirstOrDefaultAsync();
                if (offDay != null)
                    throw new Exception("Date is already an Off Day");

                var existAppointment = await _context.Appointments.Where(x => x.AppointmentDate == date).FirstOrDefaultAsync();
                if (existAppointment != null)
                    throw new Exception("There is a scheduled appointment on this day");

                OffDay newOffDay = new OffDay
                {
                    Id = Guid.NewGuid(),
                    Day = date,
                };

                await _context.OffDays.AddAsync(newOffDay);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<OffDay>> GetOffDays()
        {
            return await _context.OffDays.OrderByDescending(x => x.Day).ToListAsync();
        }

        public async Task<bool> RemoveOffDay(DateTime date)
        {
            date = date.Date;
            var offDay = await _context.OffDays.Where(x => x.Day == date).FirstOrDefaultAsync();
            if (offDay == null)
                throw new Exception("Selected Date is not an Off Day");

            _context.OffDays.Remove(offDay);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
