using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class Profiling
    {
        [Key]
        [ForeignKey("Account")]
        public string NIK { get; set; }

        [JsonIgnore]
        // one to one with Account entities
        public virtual Account Account { get; set; }

        // many to one with education
        public int EducationId { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
    }
}
