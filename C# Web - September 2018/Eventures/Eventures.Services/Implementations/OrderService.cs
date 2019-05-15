namespace Eventures.Services.Implementations
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderService : IOrderService
    {
        private readonly EventuresDbContext db;
        
        public OrderService(EventuresDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<OrderListingModel> All()
        {
            var orders = this.db.Orders;
            var result = new List<OrderListingModel>();
            var count = 1;

            foreach(var order in orders)
            {
                var customer = this.db.Users.FirstOrDefault(u => u.Id == order.CustomerId);
                var event_ = this.db.Events.FirstOrDefault(e => e.Id == order.EventId);

                var newOrder = new OrderListingModel
                {
                    Count = count,
                    EventName = event_.Name,
                    CustomerName = customer.UserName,
                    OrderedOn = order.OrderedOn
                };

                result.Add(newOrder);
                count++;
            }

            return result;
        }

        public void Order(int tickets, string username, string eventName)
        {
            var customer = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (customer == null)
            {
                throw new Exception("No such user.");
            }

            var event_ = this.db.Events.FirstOrDefault(e => e.Name == eventName);

            if (event_ == null)
            {
                throw new Exception("No such event.");
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                Event = event_,
                EventId = event_.Id,
                TicketsCount = tickets,
                OrderedOn = DateTime.UtcNow
            };

            this.db.Add(order);
            this.db.SaveChanges();
        }
    }
}