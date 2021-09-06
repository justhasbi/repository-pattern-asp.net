using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Required]
        public string Degree { get; set; }
        
        [Required]
        public string GPA { get; set; }

        // one to many with profiling
        public virtual ICollection<Profiling> Profilings { get; set; }

        // many to one with university
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual University University { get; set; }
    }
}
