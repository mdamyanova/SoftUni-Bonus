namespace Eventures.Services.Models
{
    using System;

    public class OrderListingModel
    {
        public int Count { get; set; }

        // TODO: Custom mapping
        public string EventName { get; set; }

        // TODO: Custom mapping
        public string CustomerName { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}