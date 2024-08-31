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
    [Table("OffDays", Schema = "master")]
    public class OffDay
    {
        [Required]
        [DataMember]
        public Guid Id { get; set; }
        [Required]
        [DataMember]
        public DateTime Day { get; set; }
    }
}
