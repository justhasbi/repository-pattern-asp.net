using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Context;
using NETCore.Helper;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace NETCore.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {

        private readonly MyContext myContext;

        public IConfiguration _configuration;

        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            _configuration = configuration;
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

            var accountRole = new AccountRole()
            {
                NIK = registerVM.NIK,
                RoleId = registerVM.RoleId
            };
            myContext.AccountRoles.Add(accountRole);
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

        public object Login(LoginVM loginVM)
        {
            var emailCheck = myContext.Persons.Where(x => x.Email.Equals(loginVM.Email)).FirstOrDefault();

            if (emailCheck == null)
            {
                return new
                {
                    message = "Email salah"
                };
            }
            else
            {
                var passwordCheck = myContext.Accounts.Where(x => emailCheck.NIK.Equals(x.NIK)).FirstOrDefault();

                var validatePassword = Hashing.ValidatePassword(loginVM.Password, passwordCheck.Password);
                if (emailCheck != null)
                {
                    if (validatePassword == false)
                    {

                        return new
                        {
                            message = "password salah"
                        };
                    }
                    else
                    {
                        var getRole = (from p in myContext.Persons
                                       join a in myContext.Accounts on p.NIK equals a.NIK
                                       join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                                       join r in myContext.Roles on ar.RoleId equals r.RoleId
                                       select new RoleVM
                                       {
                                           NIK = ar.NIK,
                                           RoleId = ar.RoleId,
                                           RoleName = r.Name
                                       }).Where(x => x.NIK.Equals(emailCheck.NIK)).ToList();

                        var claim = new List<Claim>
                        {
                            new Claim("NIK", passwordCheck.NIK),
                            new Claim(ClaimTypes.Email, emailCheck.Email),
                        };

                        foreach (var item in getRole)
                        {
                            claim.Add(new Claim(ClaimTypes.Role, item.RoleName));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"], 
                            _configuration["Jwt:Audience"], 
                            claim, 
                            expires: DateTime.UtcNow.AddMinutes(10), 
                            signingCredentials: signIn
                        );
                        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                        return new
                        {
                            message = "Login Sukses",
                            token = jwtToken
                        };
                    }
                }
                return new
                {
                    message = "Email salah"
                };
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
                if (emailCheck != null)
                {
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
                // email belum terdaftar
                return 0;
            }
        }
    }
}
