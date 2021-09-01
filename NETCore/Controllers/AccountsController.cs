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
            var checkEmail = myContext.Persons.Where(x => x.Email.Equals(registerVM.Email));
            var checkNIK = myContext.Persons.Where(x => x.NIK.Equals(registerVM.NIK));
            var checkPhone = myContext.Persons.Where(x => x.Phone.Equals(registerVM.Phone));

            if(checkEmail.Count() == 0 && checkNIK.Count() == 0 && checkPhone.Count() == 0)
            {
                var registerResponse = repository.Register(registerVM);
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.Created,
                    result = registerVM,
                    message = "Data Sukses Dimasukan"
                });
            } 
            else if (checkNIK.Count() > 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "NIK Sudah Digunakan"
                });
            }
            else if (checkEmail.Count() > 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "Email Sudah Digunakan"
                });
            }
            else if (checkPhone.Count() > 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "Nomor Telepon Sudah Digunakan"
                });
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                status = HttpStatusCode.InternalServerError,
                message = "Data Gagal Dimasukan"
            });

        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var loginAction = repository.Login(loginVM);

            if (loginAction == 0)
            {

                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email dan Password tidak cocok dengan database"
                });
            }
            else if (loginAction == 1)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Password salah"
                });
            }
            else if (loginAction == 2)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email Salah"
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.OK,
                    message = "Login Sukses"
                });
            }
        }

        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            repository.ForgotPassword(forgotPasswordVM);
            return StatusCode((int)HttpStatusCode.Created, new
            {
                status = HttpStatusCode.OK,
                message = "Success"
            });
        }

    }
}
