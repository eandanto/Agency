using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Infrastructure.Interfaces
{
    public interface IOffDayRepository
    {
        Task<bool> SetOffDay(DateTime date);
        Task<List<OffDay>> GetOffDays();
        Task<bool> RemoveOffDay(DateTime date);
    }
}
