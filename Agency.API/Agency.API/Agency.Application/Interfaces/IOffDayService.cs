﻿using Agency.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.Interfaces
{
    public interface IOffDayService
    {
        Task<bool> SetOffDay(OffDayDto model);
    }
}
