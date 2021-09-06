
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETCore.Models
{
    [Table("tb_m_university")]
    public class University
    {
        public int UniversityId { get; set; }

        [Required]
        public string Name { get; set; }

        // one to many with education 
        public virtual ICollection<Education> Educations { get; set; }
    }
}
