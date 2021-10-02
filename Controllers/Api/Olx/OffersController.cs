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

        public OffersController(IOlxSimulator olxSimulator)
        {
            this.olxSimulator = olxSimulator;
        }

        [HttpGet("{searchTerm}/{maxPage}")]
        public async Task<IEnumerable<MainPageOfferModel>> Get(string searchTerm, int maxPage)
        {
            return await this.olxSimulator.CollectAllOffersFor(searchTerm, maxPage);
        }
    }
}
