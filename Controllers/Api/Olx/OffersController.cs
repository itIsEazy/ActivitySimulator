namespace ActivitySimulator.Controllers.Api.Olx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX;
    using ActivitySimulator.Services.OLX.Models;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly IOlxSimulator olxSimulator;
        private readonly IOlxService olxService;

        public OffersController(
            IOlxSimulator olxSimulator,
            IOlxService olxService)
        {
            this.olxSimulator = olxSimulator;
            this.olxService = olxService;
        }

        [HttpGet("{searchTerm}/{maxPage}")]
        public async Task<IEnumerable<MainPageOfferModel>> Get(string searchTerm, int maxPage)
        {
            var models = await this.olxSimulator.CollectAllOffersFor(searchTerm, maxPage);

            await this.olxService.SaveOffersAsync(models);

            return models;
        }
    }
}
