

using ImplementCors.Base;
using ImplementCors.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.ViewModel;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository personRepository;
        
        public PersonController(PersonRepository personRepository) : base(personRepository)
        {
            this.personRepository = personRepository;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("index", "Accounts");
        }

        //[HttpPost("Register/")]
        //public JsonResult Register(RegisterVM registerVM)
        //{
        //    var result = accountRepository.Register(registerVM);
        //    return Json(result);
        //}

        [HttpGet]
        public async Task<JsonResult> GetAllPersons()
        {
            var result = await personRepository.GetAllPersons();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetSinglePerson(string id)
        {
            var result = await personRepository.GetPersonByNik(id);
            return Json(result);
        }
    }

}
