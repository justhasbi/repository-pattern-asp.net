using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
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
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    FullName = $"{p.FirstName} {p.LastName}",
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Gender = (int)p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = a.Password,
                                    Degree = e.Degree,
                                    GPA = e.GPA,
                                    UniversityId = u.UniversityId
                                }).ToList();
            return getPersonVMs;
        }

        public IEnumerable<PersonVM> GetPersonVMById(string NIK)
        {
            var getPersonVMs = (from p in myContext.Persons
                                where p.NIK == NIK
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                join prf in myContext.Profilings on
                                a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.EducationId equals e.EducationId
                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    FullName = $"{p.FirstName} {p.LastName}",
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Gender = (int)p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = a.Password,
                                    Degree = e.Degree,
                                    GPA = e.GPA
                                }).ToList();
            return getPersonVMs;
        }



        public int Register(PersonVM getPersonVM)
        {
            var person = new Person()
            {
                NIK = getPersonVM.NIK,
                FirstName = getPersonVM.FirstName,
                LastName = getPersonVM.LastName,
                Email = getPersonVM.Email,
                Phone = getPersonVM.Phone,
                BirthDate = getPersonVM.BirthDate,
                gender = (Person.Gender)getPersonVM.Gender,
                Salary = getPersonVM.Salary
            };

            myContext.Persons.Add(person);

            var account = new Account()
            {
                NIK = getPersonVM.NIK,
                Password = getPersonVM.Password
            };
            myContext.Accounts.Add(account);

            var education = new Education()
            {
                Degree = getPersonVM.Degree,
                GPA = getPersonVM.GPA,
                UniversityId = getPersonVM.UniversityId
            };
            myContext.Educations.Add(education);

            var profiling = new Profiling()
            {
                NIK = getPersonVM.NIK,
                EducationId = education.EducationId
            };
            myContext.Profilings.Add(profiling);

            var insert = myContext.SaveChanges();
            return insert;
        }
    }
}
