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
    public class OfferController : ControllerBase
    {
        private readonly IOlxSimulator olxSimulator;

        public OfferController(IOlxSimulator olxSimulator)
        {
            this.olxSimulator = olxSimulator;
        }

        // This could be changed with our own ID 
        // 1. We open and Search for some offers AND we give every offer our CURR ID 
        // 2. Then we Use this CURR ID and the model itself knows the url
        [HttpGet("{id}")]
        public async Task<OfferModel> Get(string id)
        {
            string url = "https://www.olx.bg/d/ad/hot-wheels-honda-prelude-1998g-honda-prelyud-1998g-novo-CID618-ID8Br3I.html#5e3015fe57";

            return await this.olxSimulator.OpenOffer(url);
        }

        [HttpPost("{content}/{offerId}")]
        public async Task Comment(string content, string offerId)
        {
            await olxSimulator.CommentOfferAsync(content, offerId);
        }

        [HttpPost]
        public async Task Post(OfferModel model)
        {
            await olxSimulator.SaveOfferAsync(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await olxSimulator.DeleteOfferAsync(id))
            {
                return this.NoContent();
            }

            return this.NotFound();
        }
    }
}
