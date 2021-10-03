namespace ActivitySimulator.Services.OLX.Models
{
    using System;

    public class MainPageOfferModel
    {
        public MainPageOfferModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string PriceInfo { get; set; }

        public string LocationInfo { get; set; }

        public string Url { get; set; }

        // WARNING ! This must be saved in our hard drive and sended like img NOT like URL ! ! !
        public string ImageUrl { get; set; }
    }
}
