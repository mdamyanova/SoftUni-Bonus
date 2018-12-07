using Eventures.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.Models
{
    public class EventModel
    {
        [Required]
        [MinLength(WebConstants.EventNameMinLength)]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TotalTickets { get; set; }

        public decimal PricePerTicket { get; set; }
    }
}