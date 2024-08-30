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
    [Table("Appointments", Schema = "master")]
    public class Appointment
    {
        [Required]
        [DataMember]
        public Guid Id { get; set; }
        [Required]
        [DataMember]
        public Guid CustomerId { get; set; }
        [Required]
        [DataMember]
        public string Token { get; set; }
        [Required]
        [DataMember]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [DataMember]
        public DateTime InsertedAt { get; set; }

        [ForeignKey("CustomerId")]
        public virtual User Customer { get; set; }
    }
}
