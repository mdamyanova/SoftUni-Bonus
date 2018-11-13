namespace Eventures.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UniversalCitizenNumber { get; set; }

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}