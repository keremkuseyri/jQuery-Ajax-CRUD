using jQuery_Ajax_CRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using static jQuery_Ajax_CRUD.Helper;



namespace jQuery_Ajax_CRUD.Controllers
{
    [Authorize]
    public class LoginController : Controller
    { 


        private readonly TransactionDbContext _context;

        public LoginController(TransactionDbContext context)
        {
            _context = context;
            List<LoginController> transactions = new List<LoginController>();

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }
       

    }
}