namespace SoftUni.SimpleApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string Name { get; set; }

        public IEnumerable<Journal> Journals
        {
            get
            {
                return new List<Journal>();
            }
        }
    }
}