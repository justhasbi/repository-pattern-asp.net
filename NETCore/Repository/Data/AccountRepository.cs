using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public int Register(RegisterVM registerVM)
        {
            // var hashedPassword = "";

            var person = new Person()
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                gender = (Person.Gender)registerVM.Gender,
                Salary = registerVM.Salary
            };
            var insert = myContext.SaveChanges();
            myContext.Persons.Add(person);

            var account = new Account()
            {
                NIK = registerVM.NIK,
                Password = registerVM.Password
            };
            myContext.Accounts.Add(account);
            insert = myContext.SaveChanges();

            var education = new Education()
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };

            myContext.Educations.Add(education);
            insert = myContext.SaveChanges();
            var profiling = new Profiling()
            {
                NIK = registerVM.NIK,
                EducationId = education.EducationId
            };
            myContext.Profilings.Add(profiling);
            insert = myContext.SaveChanges();

            return insert;
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

        // forgot password -> reset password
        public void ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var emailCheck = myContext.Persons.Where(x
                => x.Email.Equals(forgotPasswordVM.Email)).FirstOrDefault();

            //if email exist
            if (emailCheck != null)
            {
                // generate uid
                string guid = Guid.NewGuid().ToString();
                string stringHtmlMessage = $"Password Baru Anda: {guid}";

                // update database
                var checkEmail = myContext.Accounts.Where(e => e.NIK == emailCheck.NIK).FirstOrDefault();
                checkEmail.Password = guid;
                Update(checkEmail);

                Email(stringHtmlMessage, forgotPasswordVM.Email);
            }
        }


        public static void Email(string stringHtmlMessage, string destinationEmail)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("justhasbi7699@gmail.com");
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = "Reset Password";
            message.IsBodyHtml = true;
            message.Body = stringHtmlMessage;
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com"; //for gmail host  
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("justhasbi7699@gmail.com", "tanpabatas123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }
    }
}
