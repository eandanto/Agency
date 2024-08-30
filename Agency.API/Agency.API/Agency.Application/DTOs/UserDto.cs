using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.DTOs
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserOrCustomer { get; set; }
    }

    public class LoginDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserOrCustomer { get; set; }
    }
}
