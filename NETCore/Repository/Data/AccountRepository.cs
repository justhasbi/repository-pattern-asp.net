using NETCore.Context;
using NETCore.Helper;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

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
            var hashPassword = Hashing.HashPassword(registerVM.Password);

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
                Password = hashPassword
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

            var emailCheck = myContext.Persons.Where(x => x.Email.Equals(loginVM.Email)).FirstOrDefault();
            
            if (emailCheck == null)
            {
                return 0;
            } 
            else
            {
                var passwordCheck = myContext.Accounts.Where(x => emailCheck.NIK.Equals(x.NIK)).FirstOrDefault();
                
                var validatePassword = Hashing.ValidatePassword(loginVM.Password, passwordCheck.Password);
                if(emailCheck != null)
                {
                    if (validatePassword == false)
                    {

                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                return 0;
            }
        }

        // forgot password -> reset password
        public void ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var emailCheck = myContext.Persons.Where(x => x.Email.Equals(forgotPasswordVM.Email)).FirstOrDefault();

            //if email exist
            if (emailCheck != null)
            {
                // generate uid
                string guid = Guid.NewGuid().ToString();
                DateTime dateTime = DateTime.Now;
                string dateSend = dateTime.ToString("g");
                string stringHtmlMessage = $"Password diubah pada: {dateSend}\nPassword Baru Anda: {guid}";
                string hashPassword = Hashing.HashPassword(guid);
                // update database
                var checkEmail = myContext.Accounts.Where(e => e.NIK == emailCheck.NIK).FirstOrDefault();
                checkEmail.Password = hashPassword;
                Update(checkEmail);

                Email(stringHtmlMessage, forgotPasswordVM.Email);
            }
        }


        public void Email(string stringHtmlMessage, string destinationEmail)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("**");
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = "Reset Password";
            message.IsBodyHtml = true;
            message.Body = stringHtmlMessage;
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com"; //for gmail host  
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("**", "**");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }

        public int ChangePassword(ChangePasswordVM resetPasswordVM)
        {
            var emailCheck = myContext.Persons.Where(x => x.Email.Equals(resetPasswordVM.Email)).FirstOrDefault();

            if (emailCheck == null)
            {
                // email belum terdaftar
                return 0;
            }
            else
            {
                var passwordCheck = myContext.Accounts.Where(x => emailCheck.NIK.Equals(x.NIK)).FirstOrDefault();
                var validatePassword = Hashing.ValidatePassword(resetPasswordVM.CurrentPassword, passwordCheck.Password);

                    if (validatePassword == false)
                    {
                        // password lama salah
                        return 1;
                    }
                    else
                    {
                        // sukses update password
                        var account = myContext.Accounts.Where(x => emailCheck.NIK.Equals(x.NIK)).FirstOrDefault();
                        passwordCheck.Password = Hashing.HashPassword(resetPasswordVM.ConfirmPassword);
                        Update(account);
                        return 2;
                    }
                
            }
        }
    }
}
