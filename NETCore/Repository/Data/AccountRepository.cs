using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {

        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM getPersonVM)
        {
            var checkEmail = myContext.Persons.Where(x => x.Email.Equals(getPersonVM.Email));
            var checkNIK = myContext.Persons.Where(x => x.NIK.Equals(getPersonVM.NIK));
            var checkPhone = myContext.Persons.Where(x => x.Phone.Equals(getPersonVM.Phone));
            
            if(checkEmail.Count() == 0 && checkNIK.Count() == 0 && checkPhone.Count() == 0)
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
                var insert = myContext.SaveChanges();
                myContext.Persons.Add(person);

                var account = new Account()
                {
                    NIK = getPersonVM.NIK,
                    Password = getPersonVM.Password
                };
                myContext.Accounts.Add(account);
                insert = myContext.SaveChanges();

                var education = new Education()
                {
                    Degree = getPersonVM.Degree,
                    GPA = getPersonVM.GPA,
                    UniversityId = getPersonVM.UniversityId
                };

                myContext.Educations.Add(education);
                insert = myContext.SaveChanges();
                var profiling = new Profiling()
                {
                    NIK = getPersonVM.NIK,
                    EducationId = education.EducationId
                };
                myContext.Profilings.Add(profiling);
                insert = myContext.SaveChanges();

                return insert;
            }
            return 0;
            
        }

        public int Login(LoginVM loginVM)
        {
            var emailCheck = myContext.Persons.Where(x => x.Email.Equals(loginVM.Email));
            var passwordCheck = myContext.Accounts.Where(x => x.Password.Equals(loginVM.Password));

            if (passwordCheck.Count() == 0 && emailCheck.Count() == 0)
            {
                return 0;
            }
            else if (passwordCheck.Count() == 0)
            {
                return 1;
            }
            else if (emailCheck.Count() == 0)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
