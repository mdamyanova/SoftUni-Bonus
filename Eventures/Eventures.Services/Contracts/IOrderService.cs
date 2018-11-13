namespace Eventures.Services.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IOrderService
    {
        IEnumerable<OrderListingModel> All();

        void Order(int tickets, string username, string eventName);
    }
}