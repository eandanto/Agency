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

        public async Task<bool> SetOffDay(OffDay model)
        {
            try
            {
                var offDay = await _context.OffDays.Where(x => x.Day == model.Day).FirstOrDefaultAsync();
                if (offDay != null)
                    throw new Exception("Date is already an Off Day");

                await _context.OffDays.AddAsync(model);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
