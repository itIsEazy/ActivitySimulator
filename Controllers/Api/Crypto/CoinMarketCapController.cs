namespace ActivitySimulator.Controllers.Api.Crypto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.Crypto;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("crypto")]
    public class CoinMarketCapController : ControllerBase
    {
        private readonly ICoinMarketCapService coinMarketCapService;

        public CoinMarketCapController
            (ICoinMarketCapService coinMarketCapService)
        {
            this.coinMarketCapService = coinMarketCapService;
        }

        public async Task<string> Get()
        {
            return this.coinMarketCapService.MakeApiCall();
        }
    }
}
