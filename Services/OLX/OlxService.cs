namespace ActivitySimulator.Services.OLX
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ActivitySimulator.Data;
    using ActivitySimulator.Data.Models.Olx;
    using ActivitySimulator.Services.OLX.Models;

    public class OlxService : IOlxService
    {
        private readonly ApplicationDbContext dbContext;

        public OlxService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetOfferUrlAsync(string offerId)
        {
            return this.dbContext.Offers.Where(x => x.Id == offerId).Select(x => x.Url).FirstOrDefault();
        }

        public async Task<bool> DeleteOfferAsync(string offerId)
        {
            var offer = this.dbContext.Offers.Find(offerId);
            if (offer == null)
            {
                return false;
            }

            this.dbContext.Offers.Remove(offer);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task SaveOffersAsync(IEnumerable<MainPageOfferModel> offers)
        {
            foreach (var offer in offers)
            {
                var currOffer = new OfferModel();
                currOffer.Title = offer.Title;
                currOffer.PriceInfo = offer.PriceInfo;
                currOffer.LocationInfo = offer.LocationInfo;
                currOffer.Url = offer.Url;
                currOffer.MainImageUrl = offer.MainImageUrl;

                await this.SaveOfferAsync(currOffer);
            }
        }

        public async Task SaveOfferAsync(OfferModel offer)
        {
            var dbModelOffer = new Offer();
            dbModelOffer.Title = offer.Title;
            dbModelOffer.Description = offer.Description;
            dbModelOffer.UserName = offer.UserName;
            dbModelOffer.UserAccountUrl = offer.UserAccountUrl;
            dbModelOffer.UserPhoneNumber = offer.UserPhoneNumber;
            dbModelOffer.PriceInfo = offer.PriceInfo;
            dbModelOffer.LocationInfo = offer.LocationInfo;
            dbModelOffer.DateInfo = offer.DateInfo;
            dbModelOffer.VisitationInfo = offer.VisitationInfo;
            dbModelOffer.DeliveryConditionInfo = offer.DeliveryConditionInfo;
            dbModelOffer.Url = offer.Url;
            dbModelOffer.MainImageUrl = offer.MainImageUrl;

            await dbContext.Offers.AddAsync(dbModelOffer);
            await dbContext.SaveChangesAsync();
        }
    }
}
