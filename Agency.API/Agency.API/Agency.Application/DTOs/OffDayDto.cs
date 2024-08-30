using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.DTOs
{
    public class OffDayDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Day { get; set; }
    }
}