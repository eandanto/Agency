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
    [Table("Configurations", Schema = "master")]
    public class Configuration
    {
        [Required]
        [DataMember]
        public Guid Id { get; set; }
        [Required]
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
