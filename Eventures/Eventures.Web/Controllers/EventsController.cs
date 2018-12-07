namespace Eventures.Web.Controllers
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Contracts;
    using Services.Models;

    public class EventsController : Controller
    {
        private readonly EventuresDbContext db;
        private readonly IEventService service;

        public EventsController(EventuresDbContext db, IEventService service)
        {
            this.db = db;
            this.service = service;
        }

        public IActionResult All(int page = 1)
            => this.View(new EventListingViewModel
            {
                Events = this.service.All(page),
                TotalEvents = this.service.Total(),
                CurrentPage = page
            });

        public IActionResult MyEvents(string id)
        {
            var result = this.service.ByUser(id);

            return this.View(result);
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            return this.View();
        }
     
        [HttpPost]
        public IActionResult Create(
            [Bind("Name,Start,End,TotalTickets,PricePerTicket")] EventModel event_)
        {
            // TODO
            var eventDb = new Event
            {
                Name = event_.Name,
                Start = event_.Start,
                End = event_.End,
                TotalTickets = event_.TotalTickets,
                PricePerTicket = event_.PricePerTicket
            };

            this.service.Create(eventDb);

            return this.RedirectToAction(nameof(All));
        }
    }
}