namespace ActivitySimulator.Data.Models.Olx
{
    using System;

    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Url { get; set; }

        public string OfferId { get; set; }
    }
}
