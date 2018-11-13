namespace Eventures.Services.Implementations
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventService : IEventService
    {
        private readonly EventuresDbContext db;

        public EventService(EventuresDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EventListingModel> All()
        {
            var events = this.db.Events;
            var result = new List<EventListingModel>();
            var count = 1;

            foreach (var event_ in events)
            {
                var newEvent = new EventListingModel
                {
                    Count = count,
                    Name = event_.Name,
                    Start = event_.Start,
                    End = event_.End,
                    PricePerTicket = event_.PricePerTicket
                };

                result.Add(newEvent);
                count++;
            }

            return result;
        }

        public IEnumerable<MyEventsListingModel> ByUser(string id)
        {
            var orders = this.db.Orders.Where(o => o.Customer.Id == id);
            var result = new List<MyEventsListingModel>();
            var count = 1;

            foreach (var order in orders)
            {
                var event_ = this.db.Events.FirstOrDefault(e => e.Id == order.EventId);

                var newOrder = new MyEventsListingModel
                {
                    Count = count,
                    EventName = event_.Name,
                    Start = event_.Start,
                    End = event_.End,
                    TicketsCount = order.TicketsCount
                };

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
    }
}