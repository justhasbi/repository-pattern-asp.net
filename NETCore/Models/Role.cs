using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        // one to many with account role
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
