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
    //[EnableCors("AllowOrigin")]
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
                return NotFound(data);
                //return StatusCode((int)HttpStatusCode.NoContent, new
                //{
                //    status = HttpStatusCode.NoContent,
                //    result = data
                //});
            }
            return Ok(data);
            //return StatusCode((int)HttpStatusCode.OK, new
            //{
            //    status = HttpStatusCode.OK,
            //    result = data
            //});
        }

        [HttpGet("getperson/{NIK}")]
        public ActionResult GetPerson(string NIK)
        {
            var data = repository.GetPersonVMById(NIK);
            if (data == null)
            {
                //return StatusCode((int)HttpStatusCode.NoContent, new
                //{
                //    status = HttpStatusCode.NoContent,
                //    result = data
                //});
                return NotFound(data);

            }
            return Ok(data);

            //return StatusCode((int)HttpStatusCode.OK, new
            //{
            //    status = HttpStatusCode.OK,
            //    result = data
            //});
        }
    }
}
