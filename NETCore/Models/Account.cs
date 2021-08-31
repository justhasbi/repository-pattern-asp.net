﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("Person")]
        public string NIK { get; set; }

        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        // one to one with person entities 
        public virtual Person Person { get; set; }

        // one to one with profiling
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
    }
}
