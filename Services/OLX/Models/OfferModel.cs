using System.Collections.Generic;

namespace ActivitySimulator.Services.OLX.Models
{
    public class OfferModel
    {
        public OfferModel()
        {
            this.ImageUrls = new HashSet<string>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PriceInfo { get; set; }

        public string LocationInfo { get; set; }

        public string DateInfo { get; set; }

        public string VisitationInfo { get; set; }

        public string DeliveryConditionInfo { get; set; }

        public string Url { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }
    }
}
