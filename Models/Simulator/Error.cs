namespace ActivitySimulator.Models.Simulator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Error
    {
        public Error(string content)
        {
            this.Id = Guid.NewGuid().ToString() ;
            this.Date = DateTime.Now;
            this.Content = content;
        }

        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Reason { get; set; }

        public string Content { get; set; }
    }
}
