using Agency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Infrastructure.Interfaces
{
    public interface IOffDayRepository
    {
        Task<bool> SetOffDay(OffDay offDay);
    }
}
