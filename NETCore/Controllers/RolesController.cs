using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Models;
using NETCore.Repository.Data;

namespace NETCore.Controllers
{
    [Authorize(Roles = "Manager, HR")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository repository) : base(repository)
        {

        }
    }
}
