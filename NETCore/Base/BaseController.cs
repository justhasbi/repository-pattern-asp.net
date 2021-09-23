using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<E, R, K> : ControllerBase
        where E : class
        where R : IRepository<E, K>
    {
        private readonly R repository;

        public BaseController(R repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Insert(E entity)
        {
            try
            {
                repository.Insert(entity);
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = HttpStatusCode.Created,
                    result = entity,
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
            var data = repository.GetAll();
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

        [HttpGet("{key}")]
        public ActionResult Get(K key)
        {
            var data = repository.GetById(key);
            if (data == null)
            {
                //return StatusCode((int)HttpStatusCode.NotFound, new
                //{
                //    status = HttpStatusCode.NotFound,
                //    result = data,
                //    message = "Data tidak ditemukan!"
                //});
                return NotFound(data);
            }
            return Ok(data);
            //return StatusCode((int)HttpStatusCode.OK, new
            //{
            //    status = HttpStatusCode.OK,
            //    result = data,
            //    message = "Data ditemukan!"
            //});
        }

        [HttpPut]
        public ActionResult Update(E entity)
        {
            repository.Update(entity);
            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = HttpStatusCode.OK,
                message = "Sukses Update Data"
            });
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(K key)
        {
            try
            {
                repository.Delete(key);
                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = HttpStatusCode.OK,
                    message = "Sukses Delete Data"
                }); ;
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Gagal menghapus data"
                });
            }
        }

    }
}
