namespace ActivitySimulator.Models.Olx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX.Models;

    public class OlxOffersViewModel
    {
        public OlxOffersViewModel()
        {
            this.AllOffers = new HashSet<MainPageOfferModel>();
        }

        public IEnumerable<MainPageOfferModel> AllOffers { get; set; }
    }
}
