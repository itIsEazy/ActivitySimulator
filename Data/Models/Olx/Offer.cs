namespace ActivitySimulator.Data.Models.Olx
{
    using System;
    using System.Collections.Generic;

    public class Offer
    {
        public Offer()
        {
            this.Images = new HashSet<Image>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public string UserAccountUrl { get; set; }

        public string UserPhoneNumber { get; set; }

        public string PriceInfo { get; set; }

        public string LocationInfo { get; set; }

        public string DateInfo { get; set; }

        public string VisitationInfo { get; set; }

        public string DeliveryConditionInfo { get; set; }

        public string Url { get; set; }

        public virtual IEnumerable<Image> Images { get; set; }
    }
}
