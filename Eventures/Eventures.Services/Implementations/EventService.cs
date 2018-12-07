namespace Eventures.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Data;
    using Data.Models;
    using Eventures.Core;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventService : IEventService
    {
        private readonly EventuresDbContext db;
        private readonly IMapper mapper;

        public EventService(EventuresDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<EventListingModel> All(int page = 1)
        {
            var events = this.db.Events;
            var result = new List<EventListingModel>();
            var count = 1;

            foreach (var event_ in events)
            {
                // TODO
                var newEvent = new EventListingModel
                {
                    Count = count,
                    Name = event_.Name,
                    Start = event_.Start,
                    End = event_.End,
                    PricePerTicket = event_.PricePerTicket,
                    TotalTickets = event_.TotalTickets
                };

                result.Add(newEvent);
                count++;
            }

            return result
                .Skip((page - 1) * WebConstants.EventsPageSize)
                .Take(WebConstants.EventsPageSize);
        }

        public IEnumerable<MyEventsListingModel> ByUser(string id)
        {
            var orders = this.db.Orders.Where(o => o.Customer.Id == id);
            var result = new List<MyEventsListingModel>();
            var count = 1;

            foreach (var order in orders)
            {
                var event_ = this.db.Events.FirstOrDefault(e => e.Id == order.EventId);

                var newOrder = this.mapper.Map<MyEventsListingModel>(event_);

                count++;
                result.Add(newOrder);
            }

            return result;
        }        

        public void Create(Event model)
        {
            if (this.Exists(model.Name))
            {
                throw new Exception("Event already exists.");
            }

            this.db.Add(model);
            this.db.SaveChanges();
        }

        public bool Exists(string name)
            => this.db.Events.Any(e => e.Name == name);

        public int Total()
            => this.db.Events.Count();
    }
}