using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace NETCore.Models
{
    public class Person
    {
        [Key]
        public string NIK { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Phone]
        [Required]
        public string Phone { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public int Salary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public enum Gender { 
            Male,
            Female
        }
        public Gender gender { get; set; }

        // one to one with account
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
