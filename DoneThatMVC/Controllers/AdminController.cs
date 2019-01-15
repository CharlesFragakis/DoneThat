using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoneThatMVC.DatabaseAccess;
using DoneThatMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoneThatMVC.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {   
        
        public IActionResult Index()
        {

            return View();
        }
        
        public IActionResult AddUser()
        {

            return View();
        }
    }
}