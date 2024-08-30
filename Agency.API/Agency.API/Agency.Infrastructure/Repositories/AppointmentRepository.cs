﻿using Agency.Domain.Entities;
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


                var appointmentCount = await _context.Appointments.Where(x => x.AppointmentDate == model.AppointmentDate).CountAsync();
                if (appointmentCount > maxAppointmentPerDay)
                {
                    var isSearching = true;
                    DateTime nextAvailableDate = model.AppointmentDate;
                    while (isSearching)
                    {
                        nextAvailableDate.AddDays(1);
                        var appointmentCountDay = await _context.Appointments.Where(x => x.AppointmentDate == nextAvailableDate).CountAsync();
                        if (appointmentCountDay <= maxAppointmentPerDay)
                            isSearching = false;
                    }
                    model.AppointmentDate = nextAvailableDate;
                }

                await _context.Appointments.AddAsync(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
