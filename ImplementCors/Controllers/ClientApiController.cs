using ImplementCors.Base;
using ImplementCors.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    public class ClientApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
