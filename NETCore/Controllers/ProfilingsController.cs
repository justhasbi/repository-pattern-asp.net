using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Models;
using NETCore.Repository.Data;

namespace NETCore.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
    {
        public ProfilingsController(ProfilingRepository repository) : base(repository)
        {

        }
    }
}
