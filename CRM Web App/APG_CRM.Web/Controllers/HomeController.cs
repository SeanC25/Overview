using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APG_CRM.Web.Models;

namespace APG_CRM.Web.Controllers
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
            ViewBag.Message = "the time now is:";
            ViewBag.LongTime = DateTime.Now.ToLongDateString();
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            var formed = new DateTime(2020, 01, 01);
            var days = DateTime.Now.Subtract(formed).Days;

            /// add ViewBag for business logic to be passed to the view.
            ViewBag.formed = formed;
            ViewBag.Days = days;


            var about = new AboutViewModel
            {
                Title = "About us",
                Message = "All Purpose Glazing (APG) web portal for Customer Relationship Management (CRM) is a project dissertation which explores the application development on a ASP.NET MVC platform",
    
                Formed = new DateTime(2023, 07, 20)
            };
            return View(about);
        }


        public IActionResult ManagementActionBar()
        {
            return View();
        }

    }

}
