using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core_WebApp31.Controllers
{
    public class RoleController : Controller
    {
        public RoleController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
