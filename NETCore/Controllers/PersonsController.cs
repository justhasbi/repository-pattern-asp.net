using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Models;
using NETCore.Repository.Data;
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

        // [Authorize(Roles = "Manager")]
        [EnableCors("AllowOrigin")]
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


        [HttpGet("getperson/{NIK}")]
        public ActionResult GetPerson(string NIK)
        {
            var data = repository.GetPersonVMById(NIK);
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
    }
}
