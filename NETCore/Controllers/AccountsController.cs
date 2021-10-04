using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    //[EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {

        private readonly AccountRepository repository;
        private readonly MyContext myContext;

        public AccountsController(AccountRepository repository, MyContext myContext) : base(repository)
        {
            this.repository = repository;
            this.myContext = myContext;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var checkEmail = myContext.Persons.Where(x => x.Email.Equals(registerVM.Email)).FirstOrDefault();
            var checkNIK = myContext.Persons.Where(x => x.NIK.Equals(registerVM.NIK)).FirstOrDefault();
            var checkPhone = myContext.Persons.Where(x => x.Phone.Equals(registerVM.Phone)).FirstOrDefault();

            if(checkEmail == null && checkNIK == null && checkPhone == null)
            {
                var registerResponse = repository.Register(registerVM);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = registerVM,
                    message = "Data Sukses Ditambahkan"
                });

            } 
            else if (checkNIK != null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "NIK Sudah Digunakan"
                });
            }
            else if (checkEmail != null)
            {

                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email Sudah Digunakan"
                });
            }
            else if (checkPhone != null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Nomor Telepon Sudah Digunakan"
                });
            }
            return BadRequest(new
            {
                status = HttpStatusCode.BadRequest,
                message = "Data Gagal Dimasukan"
            });
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var loginAction = repository.Login(loginVM);
            
            return Ok(loginAction);
        }

        [HttpPut("forgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            repository.ForgotPassword(forgotPasswordVM);
            return StatusCode((int)HttpStatusCode.Created, new
            {
                status = HttpStatusCode.OK,
                message = "Success"
            });
        }


        [HttpPut("changePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var changePassAction = repository.ChangePassword(changePasswordVM);

            if (changePassAction == 0)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email Salah"
                });
            }
            else if (changePassAction == 1)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Password Lama Salah"
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.OK,
                    message = "Sukses Ubah Password"
                });
            }
        }

    }
}
