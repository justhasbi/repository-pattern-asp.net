using ImplementCors.Base;
using ImplementCors.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.ViewModel;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;

        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        // LOGIN
        [HttpPost("Auth/")]
        public async Task<IActionResult> Auth(string email, string password)
        {
            var login = new LoginVM { 
                Email = email, 
                Password = password 
            };
            
            var jwtToken = await accountRepository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public JsonResult RegisterData(RegisterClientVM registerClientVM)
        {
            var result = accountRepository.RegisterPerson(registerClientVM);
            return Json(result);
        }

        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
        
        public IActionResult Index()
        {
            var ident = User.Identity.IsAuthenticated;
            if (ident)
            {
                return RedirectToAction("Index", "home");
            }
            return View();
        }
    }
}
