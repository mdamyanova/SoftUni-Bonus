namespace Eventures.Services.Models
{
    using System;

    public class MyEventsListingModel
    {
        public int Count { get; set; }
        
        public string EventName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TicketsCount { get; set; }
    }
}