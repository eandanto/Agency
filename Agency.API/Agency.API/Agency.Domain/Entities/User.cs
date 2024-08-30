using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Domain.Entities
{
    [Serializable]
    [DataContract]
    [Table("Users", Schema = "master")]
    public class User
    {
        [Required]
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [Required]
        [DataMember]
        public string EmailAddress { get; set; }
        [Required]
        [DataMember]
        public string PasswordHash { get; set; }
        [Required]
        [DataMember]
        public string UserOrCustomer { get; set; }
    }
}
