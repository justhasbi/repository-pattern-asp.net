using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class oldPersonsController : ControllerBase
    {
        private readonly oldPersonRepository personRepository;

        public oldPersonsController(oldPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpPost]
        public ActionResult Insert(Person person)
        {
            try
            {
                personRepository.Insert(person);
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.Created,
                    data = person,
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

        [HttpGet]
        public ActionResult Get()
        {
            var person = personRepository.Get();
            if(person == null)
            {
                return StatusCode((int)HttpStatusCode.NoContent, new { 
                    status = HttpStatusCode.NoContent,
                    data = person
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = HttpStatusCode.OK,
                data = person
            });
        }

        [HttpGet ("{NIK}")]
        public ActionResult Get(string NIK)
        {

            var person = personRepository.Get(NIK);
            if(person == null){
                return StatusCode((int)HttpStatusCode.NotFound, new
                {
                    status = HttpStatusCode.NotFound,
                    result = person,
                    message = "Data tidak ditemukan!"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = HttpStatusCode.OK,
                data = person,
                message = "Data ditemukan!"
            });
        }

        [HttpPut]
        public ActionResult Update(Person person)
        {
            personRepository.Update(person);

            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = HttpStatusCode.OK,
                message = "Sukses Update Data"
            });
        }

        [HttpDelete ("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            try
            {
                personRepository.Delete(NIK);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new { 
                    status= HttpStatusCode.NotFound,
                    message ="Gagal menghapus data"
                });
            }
        }
    }
}
