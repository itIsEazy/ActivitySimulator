namespace ActivitySimulator.Services.Crypto
{
    using System;
    using System.IO;
    using System.Net;
    using System.Web;

    public class CoinMarketCapService : ICoinMarketCapService
    {
        public CoinMarketCapService()
        {

        }

        public int MyProperty { get; set; }


        public string MakeApiCall()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "2";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", this.ExtractApiKey());
            client.Headers.Add("Accepts", "application/json");

            return client.DownloadString(URL.ToString());
        }

        private string ExtractApiKey()
        {
            return File.ReadAllText(Constants.apiKeyPath); 
        }


    }
}
