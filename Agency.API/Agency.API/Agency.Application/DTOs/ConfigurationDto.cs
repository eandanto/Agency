using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.DTOs
{
    public class ConfigurationDto
    {
        public Guid Id { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}