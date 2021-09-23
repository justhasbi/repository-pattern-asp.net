

using ImplementCors.Base;
using ImplementCors.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.ViewModel;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository personRepository;
        private readonly AccountRepository accountRepository;

        public PersonController(PersonRepository personRepository, AccountRepository accountRepository) : base(personRepository)
        {
            this.personRepository = personRepository;
            this.accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register([FromBody]RegisterVM registerVM)
        {
            var result = accountRepository.Register(registerVM);
            return Json(result);
        }

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
