using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
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
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM personVM)
        {
            var registerResponse = repository.Register(personVM);
            if (registerResponse > 0)
            {
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.Created,
                    result = personVM,
                    message = "Data Sukses Dimasukan"
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "Data Gagal Dimasukan"
                });
            }
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
    }
}
