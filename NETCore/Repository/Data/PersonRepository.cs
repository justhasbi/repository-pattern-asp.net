using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, string>
    {

        private readonly MyContext myContext;

        public PersonRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<PersonVM> GetPersonVMs()
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on p.NIK equals a.NIK
                                join prf in myContext.Profilings on a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.EducationId equals e.EducationId
                                join u in myContext.Universities on e.UniversityId equals u.UniversityId
                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = $"{p.FirstName} {p.LastName}",
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Gender = (int)p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Degree = e.Degree,
                                    GPA = e.GPA,
                                    UniversityId = u.UniversityId
                                }).ToList();
            return getPersonVMs;
        }

        public PersonVM GetPersonVMById(string NIK)
        {
            var getPersonVMs = (from p in myContext.Persons
                                where p.NIK == NIK
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                join prf in myContext.Profilings on
                                a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.EducationId equals e.EducationId
                                join u in myContext.Universities on e.UniversityId equals u.UniversityId

                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = $"{p.FirstName} {p.LastName}",
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Gender = (int)p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Degree = e.Degree,
                                    GPA = e.GPA,
                                    UniversityId = u.UniversityId
                                }).ToList();
            return getPersonVMs.FirstOrDefault(x => x.NIK == NIK); 
        }
    }
}
