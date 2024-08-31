using Agency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Token { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime InsertedAt { get; set; }
    }

    public class AppointmentListDto
    {
        public List<AppointmentDto> Appointments { get; set; }
        public int TotalCounts { get; set; }
    }
}
