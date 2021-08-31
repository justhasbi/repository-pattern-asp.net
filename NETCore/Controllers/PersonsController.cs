using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using System;
using System.Net;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {

        private readonly PersonRepository repository;

        public PersonsController(PersonRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("getperson")]
        public ActionResult GetPerson()
        {
            var data = repository.GetPersonVMs();
            if (data == null)
            {
                return StatusCode((int)HttpStatusCode.NoContent, new
                {
                    status = HttpStatusCode.NoContent,
                    result = data
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = HttpStatusCode.OK,
                result = data
            });
        }



        [HttpPost("getperson")]
        public ActionResult Register(PersonVM personVM)
        {
            try
            {
                repository.Register(personVM);
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.Created,
                    result = personVM,
                    message = "Data Sukses Dimasukan"
                });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "Data Gagal Dimasukan"
                });
            }
        }


    }
}
