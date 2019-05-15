namespace Eventures.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class OrdersController : Controller
    {
        private readonly EventuresDbContext db;
        private readonly IOrderService service;
        
        public OrdersController(EventuresDbContext db, IOrderService service)
        {
            this.db = db;
            this.service = service;
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult All()
        {
            var allOrders = this.service.All();
            return this.View(allOrders);
        }

        [HttpPost]
        public IActionResult Order(int tickets, string username, string eventName)
        {
            this.service.Order(tickets, username, eventName);

            return this.RedirectToAction(nameof(All));
        }
    }
}