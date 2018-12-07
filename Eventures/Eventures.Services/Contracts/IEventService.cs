namespace Eventures.Services.Contracts
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;

    public interface IEventService
    {
        IEnumerable<EventListingModel> All(int page);

        IEnumerable<MyEventsListingModel> ByUser(string id);

        void Create(Event model);

        bool Exists(string name);

        int Total();
    }
}