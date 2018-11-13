namespace Eventures.Services.Models
{
    using System;

    public class EventServiceModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Tickets { get; set; } // Map this
    }
}