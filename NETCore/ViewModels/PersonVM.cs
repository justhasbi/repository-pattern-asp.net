using NETCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/*
 * NIK, FirstName, LastName, Phone Number, 
 * Birthdate, Gender, Salary, Email, Password, Degree, GPA
 */

namespace NETCore.ViewModel
{
    public class PersonVM
    {
        public string NIK { get; set; }

        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public int Gender { get; set; }

        public int Salary { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Degree { get; set; }

        public string GPA { get; set; }
    }
}
