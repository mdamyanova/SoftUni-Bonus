namespace Eventures.Services.Models
{
    using Core;
    using System;
    using System.Collections.Generic;

    public class EventListingViewModel
    {
        public IEnumerable<EventListingModel> Events { get; set; }

        public int TotalEvents { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalEvents / WebConstants.EventsPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage 
            => this.CurrentPage <= 1 
            ? 1 
            : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}