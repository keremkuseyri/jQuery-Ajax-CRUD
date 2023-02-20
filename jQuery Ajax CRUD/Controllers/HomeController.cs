using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using jQuery_Ajax_CRUD.Models;

namespace jQuery_Ajax_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ClassA()
        {
            return View();
        }
        public IActionResult ClassB()
        {
            return View();
        }
        public IActionResult ClassC()
        {
            return View();
        }
        public IActionResult Teacherslist()
        {
            return View();
        }
        public IActionResult TeacherslistA()
        {
            return View();
        }
        public IActionResult TeacherslistB()
        {
            return View();
        }
        public IActionResult TeacherslistC()
        {
            return View();
        }
        public IActionResult Lessonslist()
        {
            return View();
        }
        public IActionResult BulkData()
        { 
        return View();
        }
        public IActionResult Tickets()
        {
            return View();
        }
       
    }
}
