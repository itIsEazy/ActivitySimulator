namespace ActivitySimulator.Services.OLX
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX.Models;

    public interface IOlxSimulator
    {
        Task SearchInOlx();

        Task<OfferModel> OpenOffer(string url);

        Task CommentOfferAsync(string content, string offerId);

        Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm);

        Task<List<MainPageOfferModel>> CollectAllOffersFor(string searchTerm, int maxPage);
    }
}
