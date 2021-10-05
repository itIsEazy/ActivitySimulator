namespace ActivitySimulator.Services.OLX
{
    using System.Threading.Tasks;

    using ActivitySimulator.Services.OLX.Models;

    public interface IOlxService
    {
        Task SaveOfferAsync(OfferModel offer);

        Task<bool> DeleteOfferAsync(string offerId);

        Task<string> GetOfferUrlAsync(string offerId);
    }
}
