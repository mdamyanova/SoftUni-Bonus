namespace Eventures.Services.Models
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System;

    public class EventListingModel : IMapFrom<Event>
    {
        public int Count { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal PricePerTicket { get; set; }

        public int TotalTickets { get; set; }

        //public void ConfigureMapping(Profile mapper)
        //    => mapper.CreateMap<Event, EventListingModel>()
        //    .ForMember(e => e.Count, cfg => cfg.Ignore());
    }
}