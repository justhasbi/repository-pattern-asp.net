using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_tr_account_role")]
    public class AccountRole
    {
        public int AccountRoleId { get; set; }

        // many to one with account
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }

        // many to one with role
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
