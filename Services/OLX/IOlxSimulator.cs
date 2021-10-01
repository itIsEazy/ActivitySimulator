﻿namespace ActivitySimulator.Services.OLX
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX.Models;

    public interface IOlxSimulator
    {
        Task SearchInOlx();

        Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm);

        Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm, int maxPage);
    }
}
