namespace ActivitySimulator.Services.OLX.Models
{
    public class MainPageOfferModel
    {
        public MainPageOfferModel()
        {

        }

        public string Title { get; set; }

        public string PriceInfo { get; set; }

        public string LocationInfo { get; set; }

        public string Url { get; set; }

        // WARNING ! This must be saved in our hard drive and sended like img NOT like URL ! ! !
        public string ImageUrl { get; set; }
    }
}
