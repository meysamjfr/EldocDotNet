using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Application.Features.Interfaces;
using Project.Web.Experts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Experts.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IExpertService _expertService;

        public HomeController(IExpertService expertService)
        {
            _expertService = expertService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "داشبورد";

            //var model = await _expertService.DashboardStats(int.Parse(HttpContext.User.Claims.FirstOrDefault(f => f.Type == "ExpertId").Value));

            //return View(model);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
