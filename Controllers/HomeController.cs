namespace ActivitySimulator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Models;
    using ActivitySimulator.Services.OLX;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOlxSimulator olxSimulator;

        public HomeController(
            ILogger<HomeController> logger,
            IOlxSimulator olxSimulator)
        {
            _logger = logger;
            this.olxSimulator = olxSimulator;
        }

        public async Task<IActionResult> Index()
        {
            Task.Run(() => this.olxSimulator.SearchInOlx());

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
    }
}
