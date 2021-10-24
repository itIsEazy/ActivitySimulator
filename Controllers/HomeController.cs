namespace ActivitySimulator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Models;
    using ActivitySimulator.Services.Crypto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ICoinMarketCapService coinMarketCapService;

        public HomeController(
            ICoinMarketCapService coinMarketCapService)
        {
            this.coinMarketCapService = coinMarketCapService;
        }

        public async Task<IActionResult> Index()
        {
            this.coinMarketCapService.MakeApiCall();
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
