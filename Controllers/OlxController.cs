namespace ActivitySimulator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX;

    using Microsoft.AspNetCore.Mvc;

    public class OlxController : Controller
    {
        private readonly IOlxSimulator olxSimulator;

        public OlxController(IOlxSimulator olxSimulator)
        {
            this.olxSimulator = olxSimulator;
        }

        public async Task<IActionResult> Index()
        {
            Task.Run(() => this.olxSimulator.CollectAllOffersFor("lego"));

            return View();
        }
    }
}
