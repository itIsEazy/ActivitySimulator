namespace ActivitySimulator.Services.OLX
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX.Models;

    public interface IOlxSimulator
    {
        Task SearchInOlx();

        Task<OfferModel> OpenOffer(string url);

        Task SaveOfferAsync(OfferModel offer);

        Task<string> GetOfferUrlAsync(string offerId);

        Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm);

        Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm, int maxPage);
    }
}
