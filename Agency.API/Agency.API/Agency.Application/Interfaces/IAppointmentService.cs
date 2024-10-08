﻿using Agency.Application.DTOs;
using Agency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> SetAppointment(AppointmentDto model);
        Task<AppointmentListDto> GetMyAppointments(Guid id, int pageNo, int pageSize);
        Task<AppointmentListDto> GetAllAppointments(int pageNo, int pageSize, DateTime date);
    }
}
