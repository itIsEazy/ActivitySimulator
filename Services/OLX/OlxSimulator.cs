namespace ActivitySimulator.Services.OLX
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using ActivitySimulator.Models.Simulator;

    public class OlxSimulator : WebSimulator, IOlxSimulator
    {
        public OlxSimulator() : this(OlxConstants.defaultLegoSearch)
        {

        }

        public OlxSimulator(string search)
        {
            this.SearchInputs = new List<string>
            {
                search,
            };
        }

        public List<string> SearchInputs { get; set; }

        public async Task SearchInOlx()
        {
            await this.OpenOlxAsync();

            Thread.Sleep(Random.Next(1, 2) * 1000);

            Driver.Url = OlxConstants.baseSearchUrl + SearchInputs[0] + "/";

            while (true)
            {

            }
        }

        private async Task OpenOlxAsync()
        {
            Driver.Url = OlxConstants.olxUrl;
        }
    }
}
