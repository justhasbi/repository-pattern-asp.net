
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NETCore.Models
{
    public class University
    {
        public int UniversityId { get; set; }

        [Required]
        public string Name { get; set; }

        // one to many with education 
        public virtual ICollection<Education> Educations { get; set; }
    }
}
