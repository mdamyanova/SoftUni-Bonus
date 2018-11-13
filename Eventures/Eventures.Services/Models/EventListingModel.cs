namespace Eventures.Services.Models
{
    using System;

    public class EventListingModel
    {
        public int Count { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal PricePerTicket { get; set; }

        public int TicketsCount { get; set; }
    }
}